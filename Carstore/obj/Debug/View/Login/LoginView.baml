   M S B A M L   `   `   `  ����C  ?Carstore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Carstore.ViewCarstore.View  5      9!clr-namespace:Carstore.PropertiesCarstore.Properties  5      #    Carstore.View.Login.LoginView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Carstore.View     5      /p!clr-namespace:Carstore.Properties     5         UserControlStyle#��[  5	        � 	Resources  5        LoginButtonStyle �� 
TargetType  RoundedButtonStyle �� BasedOn �� Triggers ^� 
Conditions �� Binding �� ElementName �� Path  EmailConverter �� 	Converter	 �� Value
 �� Property �� Value��%/  &	       7  7  �� 5   
   " ��6)   8    5      ^� 5       5      �� 5       6$   �� $ EmailBox��$
 Text�8  	 True6�   �� 5       6$   �� $ PasswordBox��$ BorderBrush�	 Gray6b   0�5      �� 5      
 � {� . 66   5      5      5      .��5      �� $��Center=�6   $��Center=�6,   .+�5   
   �  � RowDefinitions 5       �� 5!      $��* �6    �� 5"      $��* �6    �� 5#      $��* �6    �� 5$      $��* �6    �� 5%      $��* �6    �� 5&      $��* �6     � ColumnDefinitions 5(      �� 5)      $��* �6#   �� 5*      $��* �6#   .+�5-      �� -   5        $��MessageBlock��5-   9   $��2��6   $��0��6,   $��Center=�6M   ��5.      �� 5/      " ��6   .�50      �� 
 � ��
 �  ��67   51      53      �� 55      $��Right=�6    �� Orientation$ 
Horizontal=�65   $��2��6N   $��1��6b   .+�56      �� $
��5 10q�6   #   Carstore.Properties.Resources   loginView_Email#��Z 6*   �� 57      -   5        $��EmailBox��57      $	��100��58      $
��0 10q�59      $	��Top=�5:      $	��400��5;      .��5<   3   ��5=      �� 5>      " ��6      RoundedTextBoxStyle# [ 65    5?      �� 5@   "    �� Binding 6.   �� $ EmailBox��$
 Text�# [  �� Value	 False6�   .a�5A   &   �� 
 � ��
 �  ��6D   5B   #   5D      .��5E   %   5F      5G      �� 5I      $��Right=�6   $ 
Horizontal=�65   $��2��6N   $��2��6b   .+�5J      �� $
��5 10q�6      loginView_Password#��Z 6*   G� 5K      -   5        $��PasswordBox��5K      $	��100��5L      $
��0 10q�5M      $	��Top=�5N      $	��400��5O      ��5R      �� 5S      " G�6      RoundedPasswordBoxStyle# [ 69   .�5T      �� 
 � ��
 �  ��6<   5U      5W      5X      �� 5Z      -   5        $��SavePasswordBox��5\      $��2��5Z      $��3��6+   $��Center=�68     RoundedCheckBoxStyle#��[ 5[      .��5]      �� 
�������   loginView_SavePassword#��Z 60   5^      �� 5`      -   5        $��
ExitButton��5b      $��0��5`      $��4��6%   $��10q�62   #��[ 5a      .��5c      ��    loginView_Exit#��Z 6   5d      �� 5f      -   5        $��LoginButton��5g      $��1��5f      $��4��6%   $��10q�62   ��5h      �� 5i      " ��6   # [ 60   .�5j      �� 
 � {� .  66   5k      .��5m      ��    loginView_Login#��Z 6   5n      �� 5p      -   5        $��RegisterButton��5r      $��2��5p      $��5��6)   $��10q�66   #��[ 5q      .��5s      ��    loginView_Register#��Z 6   5t      5v      5w      5x      