   M S B A M L   `   `   `  ����C  ?Carstore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Carstore.ViewCarstore.View  5      9!clr-namespace:Carstore.PropertiesCarstore.Properties  5      %    Carstore.View.NotificationsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Carstore.View     5      /p!clr-namespace:Carstore.Properties     5        � 	Resources  5         ToggleButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType  ContactsPopup��%8  &	        &	 �     �� 5   
   " d�6*   .�5      ��  � �� 5      �� 5      " d�6&   .R�5      �� 5      5      5      �� 5   
   " �6&   .�5      ��  �@�AllowsTransparency . 63   �� 5       � |� .  6*   �� 5       � ��$ Slide=�6/   5      5      .��5      �  � RowDefinitions 5!   
   �� 5"      $��50 �6   �� 5#      $��* �6   .+�5&   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5(      �� $��30��6   $��5q�5)      
���   � �� CornerRadius$ 5��5+      ._�5,      �� $ 
Horizontal=�6   .+�5-      �� -   5        $��	AllButton��5/      $��25��5-      $��2q�5.      {�.    RoundedButtonStyle5#��[ 52      .��53      �� 
�������$	��3 0q�67   #   Carstore.Properties.Resources   notificationsView_All#��Z 6D   54      �� 55      -   5        $��FromMeButton��57      $��25��55      $��2q�56      #��[ 59      .��5:      �� 
�������$	��3 0q�67   	   notificationsView_FromMe#��Z	 6D   5;      �� 5<      -   5        $��
ToMeButton��5>      $��25��5<      $��2q�5=      #��[ 5@      .��5A      �� 
�������$	��3 0q�67   
   notificationsView_ToMe#��Z
 6D   5B      5C      5D      �� 5F      -   5        $��ReloadButton��5H      $��30��5F      $��5q�5G      #��[ 5J      .��5K      �� 
�������$	��3 0q�6/      notificationsView_Reload#��Z 6<   5L      5N      &   System.Windows.Controls.DataGrid  5P   
   -   5        $��dg��5P       $��1��6   $��5q�6*   $��5q�65      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    SelectionMode$ Single=�5T         SelectionUnit$ FullRow=�5U         AutoGenerateColumns .    RoundedDataGridStyle#��[ 5X         GridLinesVisibility$
 None=�5Y         Columns 5Z      4  .System.Windows.Controls.DataGridTemplateColumn  5[      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6)      Header 5\      �� 5]        Gray#��[ 6!    �� Padding$	 5 3q�6D   $��2q�6R   $ 5��6]   ._�5^      �� $
��Bold"�6(      notificationsView_From#��Z 6:   5_         CellTemplate 5a      �� 5b      .R�5c      � .+�5d   "   d� $��UserInfo��6/   #��[  6?   .��5e   &   �� 
���������6C   �� *
UserFrom+5f   #   � 5g   "   #��[ 5h   (    � 	StaysOpen .  ��5g   (   ��  �� ElementName$ UserInfo�� �� Path$ 	IsChecked�.��5i   &   �� 
�������$ 5��5j   -   $��5q�5k   -   $ 2q�5l   -   ._�5m   *   �� .+�5n   .   �� $ 
Horizontal=�69   $��Right=�6R   .+�5o   2   �� 
�������$��Center=�5p   <      notificationsView_Email#��Z 5q   <   �� 5r   2    �� 
IsReadOnly . $��5q�6L     RoundedTextBoxStyle#��[ 5s   :   $��Center=�5t   :   $	��100��5u   :   ��5r   W   �� *UserFromEmail+5u   :   5v   /   �� 5w   .   $ 
Horizontal=�69   $��Right=�6R   .+�5x   2   �� 
�������$��Center=�5y   <      notificationsView_Phone#��Z 5z   <   �� 5{   2    . $��5q�6L   #��[ 5|   :   $��Center=�5}   :   $	��100��5~   :   ��5{   W   �� *UserFromPhone+5~   :   5   /   5�   +   5�   '   5�   #   5�      5�      5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $	 150 6@   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *UserTo+ 5�      �� 5�      #��[ 6!   $	 5 3q�6D   $��2q�6R   $ 5��6]   ._�5�      �� $
��Bold"�6(       notificationsView_To#��Z  6:   5�      5�        5�      $	 100 6>    6%   �� *Date+ 5�      �� 5�      #��[ 6!   $	 5 3q�6D   $��2q�6R   $ 5��6]   ._�5�      �� $
��Bold"�6(   !   notificationsView_Date#��Z! 6:   5�      5�        5�      $	 300 6)    5�      �� 5�      #��[ 6!   $	 5 3q�6D   $��2q�6R   $ 5��6]   ._�5�      �� $
��Bold"�6(   $"   notificationsView_Description#��Z" 6:   5�       5�      �� 5�      .R�5�      �� "# �� VerticalScrollBarVisibility$# Hidden=�6*   $$ �� HorizontalScrollBarVisibility$$ Hidden=�5�   .   
������ 
�������$��0q�5�   .    . . �5�   "   /� .T�5�   &   J� .*�60   .*�5�   *   �� �6.   �� *Description+5�   '   5�   #   5�      5�      5�        5�      $ 50 6)    5�      �� 5�      .R�5�      �� $	��New��6(   
��� �����5�   "   �� 5�   &   " ��6,   % �� Triggers% 5�   *   �� 5�   .   & �� Binding& 6:   �� *IsRead+' �� Value' True6U   .a�5�   2   ��  � y�$ Hidden=�6O   5�   /   5�   '   5�      5�      5�      5�      5�      5�      