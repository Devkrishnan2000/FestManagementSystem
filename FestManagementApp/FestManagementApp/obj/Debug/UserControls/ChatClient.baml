   M S B A M L   `   `   `  ����L  HFestManagementApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:FestManagementApp.UserControlsFestManagementApp.UserControls  5      /    )FestManagementApp.UserControls.ChatClient   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:FestManagementApp.UserControls     5      -   5        $3��-/FestManagementApp;component/Fonts/#Open Sans)�5   7   .��5	      � 
�������  � RowDefinitions  5
   
   �� 5      $��0.95* �6   �� 5      $
��0.1* �6   .+�5   
   �� -   5         ��Name$ chatbox��5      $��0��6#   ��60   �� *ChatViewModel+��5      �� 5      .R�5      �� $��chatbord��6   
������� �� CornerRadius$ 3��6?   $��1q�6P   $��	5,5,400,5q�6d    �� Padding$ 5q�6w   ._�5      � $��chatgrid��6   
�������  5      �� 5   "   $��0.20* �60   �� 5   "   $��0.05* �60   �� 5   "   $��0.70* �60   �� 5   "   $��0.05* �60    � ColumnDefinitions 5      �� 5   "   .+�5      �� $��0��6A   $��0��6N   $��10(�6^   $��	10,5,10,5q�6l   
�������$��Italic$�6�   ��6(   �� *	Usrname+6�   �� 5      $��0��6>   $��0��6K   $��10(�6[   $��	10,5,10,5q�6i   
�������$��Italic$�6�    �� TextAlignment$ Right=�6�   ��6(   �� *Rank+6�   �� 5      $��1��6(   $��0��65   $��Stretch=�6E   $
��auto��6a   $��0,0,2,2q�6n   
�������6   �� 5       $��2��6A   $��0��6N   
�������$
��Wrap=�6q   $	��0,5q�6�   ��6(   �� *	Message+6�   �� 5!      $��3��6>   $��0��6K   $��Right=�6[   $��10(�6w   
���������6(   �� *Time+6�   5"      5#       �� Triggers 5$      �� 5%       �� Value$ no��6B    �� Binding 6&   �� *	Reciver+.a�5&      �� 	 �� 
TargetName$	 chatbord��6%   
 �� Property
 � �� �� Value$ 	300,5,5,5q�6M   �� 5'      $	 chatbord��6%   
 � �� �#5352ed6Q   �� 5(      $	 chatgrid��6%   
 � �� �#5352ed6Q   5)      5+      5-      � 5/   
   -   5         �Name$ chatcontrol��5/      $��1��6$      secondary_background#���   61    50      �� 51      $��0.90* �6#   �� 52      $��0.10* �6#   .+�54      �� -   5         ��Name$ 	Inputtext��54      $��0��6)     textbox_style#���  69   $��15��6a   $��10,5,5,5q�6m   �� 55      -   5         ��Name$ sendbtn��55      $��1��6&   $��25��67   $��25��6B   
������ $��0q�6~   #���   6�   ��56      �� 57      .R�58      �� $��/Images/send.png��6    59      5;      5<      5=      5>      