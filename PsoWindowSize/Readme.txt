PSO WINDOW ASPECT RATIO CORRECT & LAUCHER
=========================================

Version 1.4.3
Author: Tulio Adriano


INDEX
=====

1. About 
2. How to use
3. Disclaimer
4. Thanks
5. Contact
6. Change History
7. Source Code


1. ABOUT
========

This program was innitialy written to help people who play PSO on a window to get the correct aspect ratio. Having been designed to work at a native resolution of 640x480, PSO looks distorted when launched at a widescreen resolution, since it tries to adapt its window size to display the game taking a good portion of the screen, relative to the screen dimensions. 

Thanks to the fact that the window borders aren't locked, it's possible to drag them until the window is comfortably squared around 4:3 but it's very hard to get it precisely to a multiple of the native resolution, and because PSO doesn't have any form of anti-alias, the pixels look out of proportion and text looks a bit wonky. 

This program is a solution to provide faster and precise resizing of PSO window to a size that will look great. It also supports free resolution, with and without aspect ratio lock so you can customize your PSO experience to what fits you best. 

Due to the good reception I was encouraged to make this program into a online/offline Launcher and eventually a Patcher, to allow playing it in full screen or Windowed mode, patch the IP address to sylverant.net, patch the keyboard fix for people running the game on Windows Vista or later, v1 and Game Cube name color, Battle Music in normal quests,  Ultimate mode map fix, and disable word filter while online. It also allows entering the serial number and access key. 


2. HOW TO USE
=============

The program is a standalone EXE that can be launched before or after PSO is running. When the program is launched, it will try to detect if PSO is running and will display the current game window size. If PSO is not running, the program will display a message and disable the resize button. Launcher and Patcher are only available if PSO is not running. Once launched there are two options:

   LAUNCHER
   ========
   
   Online
   ------
   Use this option to launch PSO with to connect to an online server.

   Offline
   -------
   Use this option to play offline and progress through the game.

   Note: When the launch button is pressed, the launcher will check if a controller is connected. If not, a message will be shown to notify that no controllers were found, thus giving you the chance to do so before proceeding.

   TOOLS
   =====

   Screenshot
   ----------
   This button will take a screenshot and save it on the Backup folder inside PSO with the same naming standard as PSO uses, in BMP format.

   Options
   -------
   Launches the PSO options screen.

   Manage Serials
   --------------
   Opens a window to allow entering or changing the Serial Number, Access Key and e-mail address. 
   

   WINDOW SIZER
   ============

   4:3 Pixel Perfect
   -----------------
   This option presents a drop down with 3 presets for the PSO window size: 1x, 2x, and 3x. Each multiplier calculates the window size based on its native 640x480 resolution. 2x looks very good on 1080 monitors or TVs.

   4:3 Screen Height
   -----------------
   Detects the height of your screen and automatically calculates the width in a 4:3 aspect ratio to fill your screen with the game window. This mode works best in Full Screen since different windows versions may reposition or resize the window if it goes larger than the screen (game field + borders). 

   Custom Size
   -----------
   This option allows you to set a free resolution. Here are the options:
   
   - Dimensions: Set the resolution to the value entered
   - Lock Ratio: If checked, it will calculate the value of the other box when a value is entered. For example: If aspect ratio is locked to 16:9, typing 1920 on the first box will automatically update the second box to 1080. Values can be entered on any box, so typing 720 on the first box with the ratio locked to 16:9 will produce a value of 1280 for the first box. 
   
   Auto Resize
   -----------
   If checked, this will enforce the selected resolution, that means that the game will automatically resize right after it's launched, and that it will restore the selected resolution if the window is manually resized.

   Launch Window Centered
   ----------------------
   If checked when the game is launched via the launcher, it will auto position PSO window at the center of your primary screen.

   Embed Full Screen
   -----------------
   This will make PSO go full screen without changing your monitor resolution. If the game window size is smaller than the screen size (pixel perfect or custom options) the game window will be centered. Otherwise it will be aligned to the top left corner of the screen. 

   Remarks: When lock ratio is not checked, any value between 1 and 8192 can be entered on either box, and the label under them will display the aspect ratio corresponding to the present values entered. 


   RUN TIME PATCHES
   ================

   Keyboard fix for Windows Vista or newer
   ---------------------------------------
   This option allows typing on the game without the use of IME, otherwise required for chat.

   PSO v1 and GC names in white
   ----------------------------
   Makes players of those versions display their names in white, they'd the same as v2 and PC otherwise.

   Battle Music in normal quests
   -----------------------------
   Allows battle music to be used in normal quests.

   Ultimate mode map fix
   ---------------------
   Fixes a bug that prevented some ultimate maps from being used on quests.

   Disable word filter
   -------------------
   Allows bad words to be displayed unfiltered in online chats.

   IP Patch
   --------
   Specify if the IP patch will be applied. If so, the server address can be entered in the box next to it. This may be useful for people wanting to test other servers. This box accepts both domain name addresses or direct IP addresses.

After the desired resolution and patch settings are selected, click Launch to start PSO. While PSO is running the resolution can be changed, but other patches will require PSO to be restarted. 


3. DISCLAIMER
=============

This program is provided as is and does not have any warranties. Use it at your own risk. This program and the developer are not affiliated or authorized by SEGA.


4. THANKS
=========

I'd like to thank everyone who is involved in keeping the PSO community alive and playing PSO online (sounds redundant, I know) possible. 
Special thanks goes to BlueCrab and Aleron Ives for all the help they gave me, and for all the hard work they put in making all of the software available to enable everyone to play PSO on Sylverant.
Well... BlueCrab, BlueCrab, BlueCrab!!! Thanks again and again and again, for giving me access to your source code that does all this patching. Without that it would have taken me several months to figure all out by myself, rather than just one day. 
More special thanks to Treamcaster for having encouraged me to bring this program to be way more than a resizer, also for the good times we spent online chatting on PSO lobby and the games we played together.


5. CONTACT
==========

I'm a member of the dcemulation forum, and you can PM me there if you'd like to talk about this program or anything else. 


6. CHANGE HISTORY
=================

1.4.4
+ Added extra options to bypass controller detection, kill PSO (in case its window is closed accidentally), and bypass patches if using a pre-patched EXE.
- Embed Fullscreen seems to be crashing PSO. This is happening to previous build as well so I suspect something changed in Windows 10 API with updates.

1.4.3
= Updated the launcher to check for DirectInput devices if no XInput devices are found, when checking for connected controllers.

1.4.2
= Small update to try to enforce PSO full screen to go over the taskbar on Windows 7.

1.4.1
- Removed Xinput 4.1 in favor to 3.1 since Windows 7 apparently doesn't have support to it.

1.4
+ Added embed full screen option for a 4:3 full screen experience.

1.3.1
+ Added a check to see if a controller is connected and notify the user if that's the case, before the game is launched.

1.3
+ Added a text box to enter what server address to connect to. 

1.2.2
= Screenshots are now always in the native resolution.
= Tab order has been fixed.

1.2.1
= Fixed a bug that caused values reloading on the custom box as they were being typed making it impossible to set a desired value.

1.2
+ Added support to patching a clean install of PSO on the fly without need of patched executables. 
+ Added an option to manage the serials from within the program.
+ Added an option to center the window when the game is launched.
= Settings are now saved
! Bug alert: When I run in full screen mode, I get a weird bug where the full screen is actually a window and it displays in a different position each run. Not sure if it's related to my code or if it's caused by Windows 10.

1.1.3
- Fixed a bug that was left from prototyping code.

1.1.2
+ Added the Options button on the tool section, that launches the PSO options screen.
+ Added an Auto Resize checkbox to allow the game to resize itself right after it's launched.
= Screenshot button now makes the camera shutter sound.

1.1.1
= Updated the exe to correct a spelling error and changed the buttons to remove the word Go. Thanks to Treamcaster for the suggestion. 

1.1
+ Added a screenshot functionality, since the windowed PSO produces damaged screenshots.
+ Added the launcher. Now you can use the program to launch PSO without the need of using other launchers.

1.0
= Initial release with window resize functionality.


7. SOURCE CODE
==============

The source code is available at https://bitbucket.org/tulioadriano/2dpsolauncher under the GNU General Public License. See <http://www.gnu.org/licenses/> for more details.


*PEACE!*
