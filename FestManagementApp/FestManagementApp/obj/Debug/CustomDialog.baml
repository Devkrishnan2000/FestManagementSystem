   M S B A M L   `   `   `  ����L  HFestManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FestManagementAppFestManagementApp  5   	   $    FestManagementApp.CustomDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:FestManagementApp     5   	     � Title$  CustomDialog��5   	   $	��200��6   $	��400��6+    � 
ResizeMode$ NoResize=�67    � WindowStyle$
 None=�6M   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6`   .��5	      �  � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      6   �� 5      $��5 �6    � ColumnDefinitions 5   
   �� 5      $��5 �6   �� 5      6!   �� 5      $��5 �6   .+�5   
   �� $��0��6   $��0��6"   $��4��62    �� Orientation$ 
Horizontal=�6F      secondary_background#���   6_   .+�5      �� -   5         ��Name$ Title��5      $��Content��6#   $��16(�65   $/��)/FestManagementApp;component/Fonts/#Medel)�6C   5      �� 5   
   $��0��6   $��1��6"   $��3��62   $ 
Horizontal=�6F   $��Right=�6_   $��0,0,10,0q�6{   .+�5      �� -   5         ��Name$ MiniBtn��5        minimize_btn_style#���  6(   $��0q�6k   $��0q�6v   �� 5      -   5        $ CloseBtn��5        close_btn_style#���  6'   $��0q�6h   $��0q�6s   5      �� 5   
   $��1��6   $��0��6   $��3��6.   $��3��6B   #���   6S   $��2q�6�   ._�5      �� $��1��6   $��1��6&   $��3��66   $ Vertical=�6K   .+�5       �� $��1��6   $��1��6*   $��2��6:   $ 
Horizontal=�6N   .+�5!      �� -   5         ��Name$
 Icon��5!      $��Images/alarm.png��6*   $��50��6D   $��50��6O   $��
20,20,0,50q�6[   	 �� BitmapScalingMode$	 HighQuality=�6o   �� 5"      -   5        
 ��Name$

 Info��5"       $��An Error Has Occured !��6.   $��
20,25,10,0q�6L   $��12(�6`   $	��300��6n   $	��250��6z   $��80��6�   $
��Wrap=�6�   5#      �� 5%      $��1��6   $��1��6*   $��3��6:   $ 
Horizontal=�6N   $��Right=�6g   .+�5&      �� -   5        $ OkBtn��5&      $��OK��6,     tab_btns#���  69   $��35��6\   $��70��6h   $��0q�6s   $��12(�6   $��0,0,20,0q�6�   6�   �� 5'      -   5        $ 	CancelBtn��5'      $��Cancel��60   #���  6A   $��35��6d   $��70��6p   $��0q�6{   $��12(�6�   $��0,0,20,0q�6�   6�   5(      5*      5,      5.      5/      