sqybi's Vimrc
=============

Introduction
------------

This is modified from the .vimrc file I used when I was working in Google.

Usage
------

1. First of all, we need latest VIM version with Python support.

   If you are using Ubuntu 20+, just install vim by `apt install vim-nox` and skip bullet #2. Otherwise,
   please follow the instrustions in bullet #2.

2. If you don't have `vim-nox` in your repository, you could build VIM with Python support by yourself.

   According to YCM's guide (https://github.com/Valloric/YouCompleteMe/wiki/Building-Vim-from-source),
   use the following command (`lua5.1-dev` should be `liblua5.1-dev` on Ubuntu 16):

   ```sh
   sudo apt-get install libncurses5-dev libgnome2-dev libgnomeui-dev \
                         libgtk2.0-dev libatk1.0-dev libbonoboui2-dev \
                         libcairo2-dev libx11-dev libxpm-dev libxt-dev python-dev \
                         python3-dev ruby-dev lua5.1 lua5.1-dev libperl-dev git
   ```

   Then remove vim if it is already existing:

   ```sh
   sudo apt-get remove vim vim-runtime gvim
   ```

   After that, `git clone` source code of VIM from `https://github.com/vim/vim.git`:

   ```sh
   git clone https://github.com/vim/vim.git
   ```

   Then update `--with-python-config-dir` and `--with-python3-config-dir` options
   in the following command, and run it.

   *Notes:*

   1. *Update the python(3)-config-dir to your Python version. Remember to check if the directory exists.*
   2. *Update the VIM version in VIMRUNTIMEDIR. Unmatched version will bring unknown error.*
   3. *Try to keep the vim source for easier installation and uninstallation.*
   
   ```sh
   cd ~/vim  # Root folder of vim source
   ./configure --with-features=huge \
                --enable-multibyte \
                --enable-rubyinterp=yes \
                --enable-pythoninterp=yes \
                --with-python-config-dir=/usr/lib/python2.7/config-x86_64-linux-gnu \
                --enable-python3interp=yes \
                --with-python3-config-dir=/usr/lib/python3.6/config-3.6m-x86_64-linux-gnu \
                --enable-perlinterp=yes \
                --enable-luainterp=yes \
                --enable-gui=gtk2 --enable-cscope --prefix=/usr
   make VIMRUNTIMEDIR=/usr/share/vim/vim81
   ```

   Then `make install` vim:
 
   ```sh
   cd ~/vim  # Root folder of vim source
   sudo make install
   ```

   Set vim as default editor:

   ```sh
   sudo update-alternatives --install /usr/bin/editor editor /usr/bin/vim 1
   sudo update-alternatives --set editor /usr/bin/vim
   sudo update-alternatives --install /usr/bin/vi vi /usr/bin/vim 1
   sudo update-alternatives --set vi /usr/bin/vim
   ```

   Now you already have VIM with latest version.

3. Install Vundle (https://github.com/VundleVim/Vundle.vim).

   ```sh
   git clone https://github.com/VundleVim/Vundle.vim.git ~/.vim/bundle/Vundle.vim
   ```

4. Copy the vimrc file to `~/.vimrc`.

5. Install plugins:

   ```sh
   vim +PluginInstall +qall
   ```
   
   To update plugins later, run:

   ```sh
   vim +PluginUpdate +qall
   ```

6. Setup YouCompleteMe:

   ```sh
   cd ~/.vim/bundle/YouCompleteMe
   ./install.py --clang-completer  # or `./install.py --all` for all languages
   ```

   See https://github.com/Valloric/YouCompleteMe for more install instructions.

7. Now everything is done! Enjoy your new VIM user interface!
