   M S B A M L   `   `   `  ����L  HFestManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FestManagementAppFestManagementApp  5   	   #    FestManagementApp.DbSelection   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:FestManagementApp     5   	     � UseLayoutRounding  .  � Title$ DbSelection��5	   	   $	��450��6   $	��800��6*    � 
ResizeMode$ NoResize=�66    � WindowStyle$
 None=�6L   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6_    � 	Resources 5
      ��%    5      .��5      � ���#5352ed � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��10 �6   �� 5      �� 5      $��10 �6    � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5   
   �� -   5         ��Name$ topbar��5      $��0��6%   $��0��62   $��5��6B    �� Orientation$ 
Horizontal=�6V      secondary_background#���   6o   .+�5      �� $��Select Database��6   $/��)/FestManagementApp;component/Fonts/#Medel)�6.   5      �� 5!   
   $��0��6   $��0��6"   $��5��62   $ 
Horizontal=�6F   $��Right=�6_   $��5,0,5,0q�6{   .+�5"      �� -   5        	 ��Name$	 MiniBtn��5"        minimize_btn_style#���  6&   6S   �� 5#      -   5        $	 CloseBtn��5#        close_btn_style#���  6'   6Q   5$      �� 5%   
   -   5        
 ��Name$
 cont1��5%      $��2��6    $��1��6-   
������� �� CornerRadius$ 5��6P   ���	#FFB7B7B7._�5&      �� $ Vertical=�6   $��10,0,0,0q�61   .+�5'      �� $��Let's Start New��6   $��30(�63   �� 5(      -   5        $
 CreateBtnBorder��5(      $��2q�63   $��	5,30,10,0q�6G   $ 3��6Z     buttonborder#���  6l   ._�5)      �� $ Vertical=�6!   .+�5*      �� $ 
Horizontal=�6%   .+�5+      �� $��Images/database.png��6$   $��50��6A   $��50��6M    J� SnapsToDevicePixels .  �� BitmapScalingMode$ HighQuality=�6s   $��5q�6�   �� 5,      $��Create New Fest ��6(   $��20(�6@   $��15,5,0,0q�6N   5-      �� 5.      $+��%Create a new fest with fresh database��6$   $��
75,-25,0,0q�6Q   5/      52      53      54      �� 57   
   -   5        $
 cont2��57      $��2��6    $��3��6-   
�������$ 5��6P   ._�58      �� $��2��6   $��3��6&   $ Vertical=�66   $��10,0,0,0q�6M   .+�59      �� $$��Select from Existing Database ��6   $��25(�6B   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6P   �� 5:      $0��*Select existing Fest from the list bellow:��6   $��15(�6N   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6\   $��	10,20,0,0q�6�   �� 5;      -   5         ��Name$ 	DblistBox��5;      ��6-   �� *DblistModel+��5<      �� 5=      .R�5>      �� $ 
Horizontal=�6)   .+�5?   "   �� $��Images/databaseS.png��6(   $��20��6F   $��20��6Q   �� 5@   "   ��6,   �� *Dbname+5A      5B      5D      �� 5E      -   5        $	 NextBtn��5E      $
��Next��6*     tab_btns#���  69   $��80��6\   $��30��6g   $��12(�6s   $��0q�6�   $��Right=�6�   $��0,0,10,0q�6�   6�   5F      5G      5J      5K      