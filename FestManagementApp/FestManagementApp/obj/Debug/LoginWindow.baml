   M S B A M L   `   `   `  ����L  HFestManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FestManagementAppFestManagementApp  5   	   "    FestManagementApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   
   +localclr-namespace:FestManagementApp  5   	     � Title$  Login��5	   	   $	��500��6   $	��450��6$    � 
ResizeMode$ NoResize=�60    � WindowStyle$
 None=�6F   .��5
      �    primary_color_btn#���   6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      6!   �� 5      $��10 �6   .+�5   
   �� -   5         ��Name$ TopBar��5      $��0��6%   $��0��62   $��4��6B     primary_background#���  6V    �� Orientation$ 
Horizontal=�6�   .+�5      �� $��Login��6   $/��)/FestManagementApp;component/Fonts/#Medel)�6%   $��10,0,0,0q�6\   $��16(�6n   $��Center=�6|   5      �� 5   
   $��0��6   $��1��6"   $��3��62   $ 
Horizontal=�6F   $��Right=�6_   $��0,0,10,0q�6{   .+�5      �� -   5         ��Name$ MiniBtn��5        minimize_btn_style#���  6(   $��0q�6k   $��0q�6v   �� 5      -   5        $ CloseBtn��5        close_btn_style#���  6'   $��0q�6h   $��0q�6s   5      �� 5   
   $��LOGIN TO SERVER��6   $��1��6*   $��1��67   $��40(�6G   
�������$��Center=�6h   $/��)/FestManagementApp;component/Fonts/#Medel)�6�   �� 5    
   -   5         ��Name$ 
Login_cont��5       $��1��6(   $��1��65   
�������	 �� RadiusX$	 10��6R   
 �� RadiusY$
 10��6_   $��50,70,50,75q�6l   �� 5!   
   -   5        $ Login��5!      $��1��6$   $��1��61   $ Vertical=�6A   $��70,90,70,65q�6X   .+�5#      �� $��Server Name��6   $��1��6+   $��1��68   $��15(�6H   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6V   �� 5$      -   5         ��Name$ 
ServerName��5$        textbox_style#���  6*   $��
0,10,10,10q�6R   �� 5&      $��	User Name��6   $��1��6)   $��1��66   $��15(�6F   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6T   �� 5'      -   5        $ UserName��5'      #���  6(   $��
0,10,10,10q�6P   �� 5)      $��Password��6   $��1��6(   $��1��65   $��15(�6E   $3��-/FestManagementApp;component/Fonts/#Open Sans)�6S   G� 5*      -   5         G�Name$ Passwrd��5*        paswrd_style#���  6+   $��
0,10,10,10q�6S   �� 5,      -	   5        $ CntBtn��5,      $��1��6%   $��1��62   $��Connect��6B     tab_btns#���  6T   $��Center=�6w   $��50��6�   $	��200��6�   $��15(�6�   6�   5.      �� 5/   
   -
   5         ��Name$ loading��5/   �   "  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Animations/Loading.gif��6   $��1��6J   $��1��6W   $��99.6,386,100,14q�6g   $y�	Collapsed=�6�   �� 50   
   -   5         ��Name$ 
loadingtxt��50      $��Connecting..��6$   $��1��6;   $��1��6H   $��Center=�6X   
�������$��176.6,437.2,178,-417q�6�   $��2��6�   $y�	Collapsed=�6�   �� 51   
   -   5        $ CntInfoCont��51      $��2��6*   $��0��67   $��4��6G   
�������$��Center=�6n   $y�	Collapsed=�6�   .+�52   
   �� -   5        $ CntInfo��52      $��
Connecting��6!   54      56      57      