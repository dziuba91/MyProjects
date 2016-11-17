
README
------

This is README.txt file of project: "telesim".

Program was created as project for course: Application Development
Course details: IITA0202-5 Application Development (I-IT-2N1)

Project was finalized in 15.12.2014,
by: Tomasz Dziuba


CONTENTS OF THIS FILE
---------------------
  
* Introduction
* Design
* File list
* Requirements
* Installation
* Troubleshooting
* Contact information


I. INTRODUCTION
---------------

Program simulates "Teletraffic":

* A local switch is connected to a number of telephones (100)

* Each telephone (if in idle) has probability of 0.001 to generate a call. 
    The call duration is uniformly distributed in 20 sec and 200 sec.
    
* The local switch needs to use a trunk (10 in total) to carry to call out 
    (to the called party). When the carried call is release, this trunk can 
    be reused.
    
* If all the trunks are taken, the new generated called are blocked.


II. DESIGN
----------

Main screen display:
  
* information subtitles on the top of screen, which contain:
    - current time of the simulation
    - number of phones with state BUSY
    - number of blocked phones
    - number of succeded phones

* main simulation screen:
        + telephones are presented in frame: if current telephone is busy frame
          is red, if current telephone is idle frame is white
        + every telephones have own frame (on the screen is 100 frames)
        + frames are display in 5 rows and 10 columns
    
* information subtitles about trunks, on the down of screen, which contain:
    - number of current trunks
    - number of phone which use the trunk and remained time
        (if trunk state is in idle just "IDLE" is displayed)

        
III. FILE LIST
--------------

Program files are archived into - telesim.tar.gz. The content of this folder
includes source code of program:

* main.c		contain main function which show the general logics of this 
                program
                
* screen.c		contain graphics function which allow to display on the screen
                program output
                
* screen.h		it's a library file of screen.c,
                contain: constant variable definitions, macro definitions, 
                    enumerations and function prototypes

* telesim.c		contain functions of the simulation - this functions counts and 
                setting required members of the structures which control the
                simulation
                
* telesim.h		it's a library file of telesim.c,
                contain: constant variable definitions, macro definitions, 
                    enumerations, function prototypes and structure definition
                
* makefile		describe compiling process - allow to compile all source code
                into an executable file (program: teleApp.a) 
                - to create program use command "make" on your terminal

                
IV. REQUIREMENTS
----------------

* Operating System:
    + Program is dedicated to devices and computers using: Linux OS.
    + It's possible also to use devices with other similar to Linux OS
        operating systems (like "Raspberry PI").
        
* Tools:
    + On Linux OS use: "Terminal".
    + You can also connect to devices which using Linux OS (or similar) 
        by "Putty" program and run application as well.
        
* Graphics requirements:
    + Program for displaying frames use UNICODE. Make sure that your tool
        recognise UNICODE characters.
    + Set the hight and width of the screen to the width and hight of the 
        simulation screen before you start the program.


V. INSTALLATION
---------------

* In Linux OS open Terminal and use command "cd" to set required folder
    (You can use also "Putty" to connect with servers and devices which 
     using Linux similar systems e.g. raspberry Pi).
    
* If folder is archived (.tar.gz) to unpack folder with program code use 
    command:

    ---> tar -xf telesim.tar.gz

* Use comment "make" to create program file

* To run program use comment:

    ---> ./teleApp.a


VI. TROUBLESHOOTING
-------------------

* Unrecognised characters are displaying:
    + Make sure that on your Terminal is possible to display UNICODE character -
        check settings of Terminal (or Putty) an check if "Remote Character" is 
        set to "UTF-8".
    
* Some of characters are displaying in wrong place:
    + Make sure that your screen have set correctly width and height, and it's
        possible to display all simulation screen in this window
        - to fix this just change screen sizes (Terminal or Putty sizes) 
            and run program again.
        
    
VII. CONTACT INFORMATION
-----------------------

For more information about this program contact:

    Tomasz Dziuba
    mail: kira.dziuba333@gmail.com
