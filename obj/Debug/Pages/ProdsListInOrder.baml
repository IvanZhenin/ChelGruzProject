   M S B A M L   `   `   `  ����D  @GruzChelb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GruzChelb.PagesGruzChelb.Pages  5      &     GruzChelb.Pages.ProdsListInOrder   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:GruzChelb.Pages     5      -   5          N� Title$  ProdsListInOrder��5	      .��5
      �  � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $
��445* �6   �� 5      $��65 �6   .+�5   
   �� $��0��6   
�������$��3��64   
���   �$��3q�5       �� CornerRadius$ 15��6%   $��0 0 0 12q�67   �� 5   
   $��1��6   
���   �$��3q�62   
�������._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridProds��5         AutoGenerateColumns .  
������    
IsReadOnly .    CanUserResizeRows .     CanUserReorderColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Товар��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 180 5   )      MinWidth$	 180Q�65      
FontFamily$ Verdana)�6D      FontSize$ 16Q�5   )      
FontWeight$ Medium"�67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   8   �� *Products.Name+5   7     5      $	 Количество��6)   $	
 130 5   )   $	 130Q�65   $ Verdana)�6D   $ 16Q�5   )   $ Medium"�67    5   =   �� *
Quantity+5   7     5      $"	 Цена за единицу��6)   $	
 150 5   )   $	 150Q�65   $ Verdana)�6D   $ 16Q�6Y   $ Medium"�5   )    5   B   �� *Products.PriceOne+ �� StringFormat$ {0:0} руб.��5   )   4  .System.Windows.Controls.DataGridTemplateColumn  5       $	
 120 6-   $	 120Q�69      CellTemplate 5!      �� 5"      .R�5#   "   �� -   5        $��Изменить��5#   )   $��Verdana)�6Q   $��16(�5$   )   $��Medium"�67   
��� ���6K   5%      5'      5)      5*      �� 5,   
   -   5        $��2��5,      $��Добавить��6   $
��Left=�61      buttonTransTwo#��[  5-      $	��200��69   $��	20 0 0 15q�5.      6$   �� 5/   
   -   5        $��2��5/      $��Удалить��6   $��Right=�60   #��[  50      $	��200��69   $��	0 0 20 15q�51      6$   52      53      