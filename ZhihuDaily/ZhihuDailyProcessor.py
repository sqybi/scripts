import datetime
import http.client
import json
import re
import os
import time

# Zhihu Daily API: https://github.com/izzyleung/ZhihuDailyPurify/wiki/知乎日报-API-分析

startDate = datetime.date(2013, 5, 19)
endDate = datetime.date.today()
dateInterval = datetime.timedelta(days = 1)
print('Loading article ids from', startDate, 'to', endDate)

currentDate = startDate
connection = http.client.HTTPConnection('news-at.zhihu.com')
storyIdList = []
while currentDate <= endDate:
    currentDate += dateInterval
    currentDateString = currentDate.strftime('%Y%m%d')
    queryUri = '/api/3/news/before/' + currentDateString

    connection.request('GET', queryUri)
    response = connection.getresponse()
    data = response.read().decode('utf-8')

    jsonObject = json.loads(data)
    for story in jsonObject['stories']:
        storyId = str(story['id'])
        storyIdList.append(storyId)
        print('Get storyId for date', currentDate - dateInterval, ':', 
storyId)

    time.sleep(1)
connection.close()

connection = http.client.HTTPConnection('daily.zhihu.com')
regex = re.compile(r'.*?<span class="author">(?P<author>.*?)</span>.*', 
re.S)
storyInformation = {}
for storyId in storyIdList:
    queryUri = '/story/' + storyId

    connection.request('GET', queryUri)
    response = connection.getresponse()
    data = response.read().decode('utf-8')

    result = regex.match(data)
    if result:
        author = result.group('author')
        if author[-1] == '，':
            author = author[:-1]
        storyInformation[storyId] = {'author': author, 'data': data}
    else:
        storyInformation[storyId] = {'author': None, 'data': data}

    storyInformationJson = json.dumps(storyInformation[storyId], indent 
= 4)
    file = open(storyId + ".txt", 'w')
    file.write(storyInformationJson)
    file.close()

    if author == 'sqybi':
        print('Story', storyId, 'is authored by sqybi')

    time.sleep(1)

