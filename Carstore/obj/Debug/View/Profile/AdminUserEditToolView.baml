   M S B A M L   `   `   `  ����C  ?Carstore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Carstore.ViewCarstore.View  5      9!clr-namespace:Carstore.PropertiesCarstore.Properties  5      1    +Carstore.View.Profile.AdminUserEditToolView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Carstore.View     5      /p!clr-namespace:Carstore.Properties     5      -   5          � 	Resources  5         MoreRoundedButtonStyle �� 
TargetType  RoundedButtonStyle �� BasedOn �� 	Resources �� Property �� Value��%�   &	        7  �� 5   
   " ��6/   8    5      ���� 5      '��       " ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $
��Left=�6;   $��Center=�6V   .+�5       �� -   5        $��CancelButton��5       #��[  6)   $	��100��5!      $��30��6!   $��10q�6-   .��5#      �� #   Carstore.Properties.Resources*   #adminUserEditToolView_CancelChanges#��Z 6   5$      �� 5%      -   5        $��
SaveButton��5%      #��[  6'   $	��100��5&      $��30��6!   $��10q�6-   {�.  .��5)      �� (	   !adminUserEditToolView_SaveChanges#��Z	 6   5*      �� 5+      
 �� CornerRadius$
 5��6   
���   �$��10q�5,      ._�5-      �� $ 
Horizontal=�6   .+�5.      �� -   5        $��
UserButton��5.      #��[  6/   $	��100��5/      $��25��6)   $��2q�65   {�.  .��52      �� !   adminUserEditToolView_User#��Z 6$   53      �� 54      -   5        $��ModeratorButton��54      #��[  64   $	��100��55      $��25��6)   $��2q�65   {�.  .��58      �� &   adminUserEditToolView_Moderator#��Z 6$   59      5:      5;      5<      �� 5>   
   $��1��6   $ 
Horizontal=�6"   $
��Left=�6;   $��Center=�6V   .+�5?      �� 
�������$��10 5 5 5q�5@      !   adminUserEditToolView_Name#��Z 5A      �� 5B      -   5        $��NameBox��5B        RoundedTextBoxStyle#��[ 5C      $��5q�5D      $	��150��5E      5F      5G      &   System.Windows.Controls.DataGrid  5I   
   -   5        $��dg��5I       $��2��6   $��5q�6*   $��5q�65      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    SelectionMode$ Single=�5M         SelectionUnit$ FullRow=�5N         AutoGenerateColumns .    RoundedDataGridStyle#��[ 5Q         GridLinesVisibility$
 None=�5R         Columns 5S      4  .System.Windows.Controls.DataGridTemplateColumn  5T      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 300 6)      Header 5U      �� 5V        Gray#��[ 6!    �� Padding$	 5 3q�6D   $��2q�6R   $
 5��6]   ._�5W      �� $
��Bold"�6(   #��Z 6:   5X         CellTemplate 5Z      �� 5[      .R�5\      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5]   "   A� $��SellerPhotoField��6*   $��20��6B   $��20��6M   $��0 0 5 0q�5^   *   $��Center=�5_   *   ��5`   &   �� 5a   *    j� Stretch$
 Fill=�6S    �� ImageSource 65   �� *Photo+6S   5c   #   �� 5d   "   $��Center=�5e   ,   ��5d   ,   �� *Name+ � ToolTip 5f   ,   �� *Email+5g      5h      5j      0  *System.Windows.Controls.DataGridTextColumn  5k      $	 150 6l   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Role+  LocalizeConverter �� 	Converter# [  5l      �� 5m      #��[ 6!   $	 5 3q�6D   $��2q�6R   $
 5��6]   ._�5n      �� $
��Bold"�6(   !   adminUserEditToolView_Role#��Z 6:   5o      5q      5t      5v      5w      