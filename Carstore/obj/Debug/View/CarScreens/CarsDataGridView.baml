   M S B A M L   `   `   `  ����C  ?Carstore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Carstore.ViewCarstore.View  5      9!clr-namespace:Carstore.PropertiesCarstore.Properties  5      C&clr-namespace:Carstore.View.ComponentsCarstore.View.Components  5      /    )Carstore.View.CarScreens.CarsDataGridView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Carstore.View     5      /p!clr-namespace:Carstore.Properties     5      =
components&clr-namespace:Carstore.View.Components     5      ���#232323.��5      � -   5        $��CarsGrid��5      .+�5   
   � -   5        $��DGGrid��5        � RowDefinitions  5      �� 5      $��50 �6    �� 5      $��50 �6    �� 5      $��* �6    .+�5      �� $��2��6   $��5q�6&    �� CornerRadius$ 10��61    �� Padding$ 5q�6C   ���#7070706O   �� 5      $��0��6    �� Orientation$ 
Horizontal=�6&   $��5q�6?   .+�5      �� -   5        $��ResetButton��5      $��75��6,   $��25��67   $��5q�6C      RoundedButtonStyle#��[  5      .��5      �� #   Carstore.Properties.Resources   carsDataGridView_Reset#��Z 6    5      �� 5       $��Center=�6   
�������$��5 0 0 0q�5"         carsDataGridView_Mark#��Z 5#      � 5$      .+�5%      �� -   5        $��MarkBox��5%      $	��175��6.   $��25��6:   $��5q�6F     RoundedComboBoxStyle#��[ 5&       �� IsTextSearchEnabled .  �� DisplayMemberPath$
 Name��5*       �� SelectedValuePath$
 Name��64   �� 5+        DarkGray#��[ 6   $ 3��5,      $��8 8 25 8q�5-      y�5.      �� 	 �� ElementName$	 MarkBox��
 �� Path$
 IsDropDownOpen�  BoolToVisibilityConverter �� 	Converter# [ ._�5/      �� -   5        $��MarkSearchBox��5/   $   $��Center=�50       
�������
������ 52       53      54      �� 56      $��Center=�6   
�������$��5 0 0 0q�58         carsDataGridView_Model#��Z 59      � 5:      .+�5;      �� -   5        $��ModelBox��5;      $	��175��6/   $��25��6;   $��5q�6G   {�.   . #��[ 5?      $
 Name��5@      $
 Name��60   �� 5A      #��[ 6   $ 3��5B      $��8 8 25 8q�5C      y�5D      �� $	 ModelBox��$
 IsDropDownOpen�# [ ._�5E      �� -   5        $��ModelSearchBox��5E   $   $��Center=�5F       
�������
������ 5H       5I      5J      �� 5L      $��Center=�6   
�������$��5 0 0 0q�5N         carsDataGridView_Type#��Z 5O      � 5P      .+�5Q      �� -   5        $��TypeBox��5Q      $	��150��6.   $��25��6:   $��5q�6F    . #��[ 5T      $
 Name��5U      ��5V      �� 5W      .R�5X   "   �� ��6,   �� *Name+  LocalizeConverter# [ 5Y      5[      �� 5\      #��[ 6   $ 3��5]      $��8 8 25 8q�5^      y�5_      �� $	 TypeBox��$
 IsDropDownOpen�# [ ._�5`      �� -	   5        $��TypeSearchBox��5`   $   $��Center=�5a       
�������
������ 5c       5d      5e      5g      �� 5i      $��1��6   $ 
Horizontal=�6&   $��5q�6?   .+�5k      �� -
   5        $��SearchButton��5k      $��75��6-   $��25��68   $��5q�6D   #��[  5l      .��5n      ��    carsDataGridView_Search#��Z 6    5o      �� 5q      -   5        $��ShowMineBox��5s      $	��125��5q      $��25��6'   $��5q�5r      
�������  RoundedCheckBoxStyle#��[ 5u      .��5v      ��     carsDataGridView_ShowMine#��Z 6    5w      �� 5y      $��Center=�6   
�������$��5 0 0 0q�5{         carsDataGridView_Price#��Z 5|      �� 5}      $��30��6   $	��210��5~      $ 2q�5      $��5q�5�      $ 7��5�      
�������._�5�      �� $ 
Horizontal=�6!   .+�5�      ,   &Carstore.View.Components.NumericUpDown  -   5          Name$ PriceMinBox��5�   3   $��Center=�6H   $
��Left=�6c   $��0 0 3 0q�6~   $	��100��5�   3       carsDataGridView_MinPrice � ToolTip# Z 5�   3      Maximum 5�   3   �� $	 PriceMaxBox��$
 Value�  5�      -   5        $ PriceMaxBox��5�   3   $��Center=�6H   $��Right=�6c   $��3 0 0 0q�6   $	��100��5�   3       carsDataGridView_MaxPrice# Z 5�   3      Minimum 5�   3   �� $	 PriceMinBox��$
 Value�5�      5�      �� 5�      $��Center=�6   
�������$��5 0 0 0q�5�         carsDataGridView_Power#��Z 5�      �� 5�      $��30��6   $	��210��5�      $ 2q�5�      $��5q�5�      $ 7��5�      
�������._�5�      �� $ 
Horizontal=�6!   .+�5�        -   5        $ PowerMinBox��5�   3   $��Center=�6H   $
��Left=�6c   $��0 0 3 0q�6~   $	��100��5�   3       carsDataGridView_MinPower# Z 5�   3    5�   3   �� $	 PowerMaxBox��$
 Value�  5�      -   5        $ PowerMaxBox��5�   3   $��Center=�6H   $��Right=�6c   $��3 0 0 0q�6   $	��100��5�   3   # Z 5�   3    5�   3   �� $	 PowerMinBox��$
 Value�5�      5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5        $��dg��5�   $   $��3��6   $��5q�6.   $��5q�69      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    SelectionMode$ Single=�5�         SelectionUnit$ FullRow=�5�         AutoGenerateColumns .    RoundedDataGridStyle#��[ 5�         GridLinesVisibility$
 None=�5�          Columns  5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn!   Width5  /System.Windows.Controls.DataGridLengthConverter$	! 100 6-   "   Header" 5�      �� 5�        Gray#��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   #   carsDataGridView_Photo#��Z# 6>   5�      $   CellTemplate$ 5�      �� 5�      .R�5�   "   �� $ 2��6)   ��5�   &   �� 5�   *   % �� ImageSource% 65   �� $
 Photo�._�5�   &   �� $y�Hidden=�6J   ��6,   �� $
 Photo�6J   5�   #   5�      5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $	! 100 6G   1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6)   �� $

 Mark�" 5�      �� 5�      #��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   "'   carsDataGridView_MarkHeader#��Z' 6>   5�      5�        5�      $	! 100 6H   & 6)   �� $
 Model�" 5�      �� 5�      #��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   #(   carsDataGridView_ModelHeader#��Z( 6>   5�      5�        5�      $	! 100 6H   & 6)   �� $
 Color�" 5�      �� 5�      #��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   #)   carsDataGridView_ColorHeader#��Z) 6>   5�      5�        5�      $	! 100 6H   & 6)   �� $
 Price�" 5�      �� 5�      #��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   #*   carsDataGridView_PriceHeader#��Z* 6>   5�      5�        5�      $	! 100 6H   & 6)   �� $
 Power�" 5�      �� 5�      #��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   #+   carsDataGridView_PowerHeader#��Z+ 6>   5�      5�        5�      $	! 150 6I   & 6)   �� $
 Seller�" 5�      �� 5�      #��[ 6%   $	 5 3q�6H   $��2q�6V   $ 5��6a   ._�5�   "   �� $
��Bold"�6,   ,   carsDataGridView_Seller#��Z, 6>   5�      5�      5�      5�      5�      5�      