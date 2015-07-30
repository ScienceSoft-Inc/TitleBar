ScnTitleBar
======================
Customizable Xamarin.Forms title bar for Android, iOS and Windows Phone: place up to 5 buttons inside it and position/paint the bar as you want.

Title Bar Control Structure
===========================================
The control may have the following items:
- Back button
- 1st left button
- 2nd left button
- Title
- 1st right button
- 2nd right button
 
You can put control in any place on a page and choose which items (out of the above list) to show. You may change icons, colors, title text and it's style. The "Back" button implements standard navigation action of each platform.

See schemas bellow:

![Main](Screenshots/Droid/SampleTitleBar.png)

Bellow is shown screenshots from real application.
You can find sources of this application here: https://github.com/ScienceSoft-Inc/XamarinDiscountsApp.
![Main](Screenshots/Droid/DiscountsAppTitleBar.png)

How to use this control in Xamarin.Forms app
===========================================
ScnTitleBar constuctor includes three parameters:
- Owner page;
- Kind of view buttons;
- Control alignment (only for renderer not for location on a page).

Look sample to know how right include control in your application.

If you want to have responsive buttons on tap then need to add initialize renderers for each platform.

In iOS project just use
```cs
Xamarin.Forms.Forms.Init ();
ViewGesturesRenderer.Init();
```
In Android project just use
```cs
Xamarin.Forms.Forms.Init (this, bundle);
ViewGesturesRenderer.Init();
```
In WinPhone project just use
```cs
Xamarin.Forms.Forms.Init ();
ViewGesturesRenderer.Init();
```
