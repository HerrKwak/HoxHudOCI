# HoxHud One-Click Installer
## Why?
I have created this lil piece of software to build a hazzle free installation and updating enviroment for endusers.

## How?
1. Either compile it from source or download it ~~here~~
2. **Open** the program
    1. It will **automatically** check if Payday 2 is installed
    2. It will then check where it is installed
       1. If it **cannot find** the folder it will **prompt** the user to enter it manually
3. If you already had HoxHud installed
   * It will have a **checkbox** for you to specify wether or not it should **replace HoxHudTweakData.lua** (the option storage file)
   * Later on when copying the files to the game folder, it will make a **backup** of the previous files in the hoxhud folder (called *$filename*__.backup__)   
4. Then just **hit Install** it will then:
   1. **Download the master branch** from [HoxHud's Github](https://github.com/HoxHud/HoxHud-bin) (it will save to a random filename)
   2. **Unpack** that Folder to the folder where the Application runs
   3. **Move the files** over to the game directory
   4. Delete the created folder and file

### TODO's
* Upload a binary *somewhere*
* Make the code less hacky
* Add a wee bit more doc
* Handle IO stuff better
* make this readme look better