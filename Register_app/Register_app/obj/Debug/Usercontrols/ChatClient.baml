   M S B A M L   `   `   `  ����G  CRegister_app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Register_app.UsercontrolsRegister_app.Usercontrols  5      *    $Register_app.Usercontrols.ChatClient   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Register_app.Usercontrols     5      
�������.��5	      �   � RowDefinitions  5
   
   �� 5      $��0.95* �6   �� 5      $
��0.1* �6   .+�5   
   �� -   5         ��Name$ chatbox��5      $��0��6#   ��60   �� *ChatViewModel+��5      �� 5      .R�5      �� $��chatbord��6   
������� �� CornerRadius$ 3��6?   $��0q�6P   $��	5,5,400,5q�6d    �� Padding$ 5q�6w   ._�5      �  �Name$ chatgrid��6   
�������  5      �� 5   "   $��0.20* �60   �� 5   "   $��0.05* �60   �� 5   "   $��0.70* �60   �� 5   "   $��0.05* �60    � ColumnDefinitions 5      �� 5   "   .+�5      ]� $��0��6(   $��0��65   .+�5   "   �� $��0��6E   $��0��6R   $��10(�6b   $��	10,5,10,5q�6p   
�������$
��Bold"�6�   ��6,   �� *	Usrname+6�   �� 5   "   $��0��6B   $��0��6O   $��10(�6_   $��	10,5,10,5q�6m   
�������$��Italic$�6�    �� TextAlignment$ Right=�6�   ��6,   �� *Rank+6�   5       �� 5"      $��1��6(   $��0��65   $��Stretch=�6E   $
��auto��6a   $��0,0,2,2q�6n   
�������6   �� 5#      $��2��6A   $��0��6N   
�������$
��Wrap=�6q   $	��0,5q�6�   ��6(   �� *	Message+6�   �� 5$      $��3��6>   $��0��6K   $��Right=�6[   $��10(�6w   
���������6(   �� *Time+6�   5%      5&       �� Triggers 5'      �� 5(       �� Value$ no��6B   	 �� Binding	 6&   �� *	Reciver+.a�5)      �� 
 �� 
TargetName$
 chatbord��6%    �� Property � �� �� Value$ 	300,5,5,5q�6M   �� 5*      $
 chatbord��6%    � �� �#5352ed6Q   �� 5+      $
 chatgrid��6%    � �� �#5352ed6Q   5,      5.      50      � 52   
   -   5        $ chatcontrol��52      $��1��6$      secondary_background#���   61    53      �� 54      $��0.90* �6#   �� 55      $��0.10* �6#   .+�57      �� -   5         ��Name$ 	Inputtext��57      $��0��6)     textbox_style#���  69   $��20��6a   $��10,5,5,5q�6m   �� 58      -   5         ��Name$ sendbtn��58      $��1��6&   $��25��67   $��25��6B   
������ $��0q�6~   #���   6�   ��59      �� 5:      .R�5;      �� $��/resources/icons/send.png��6    5<      5>      5?      5@      5A      