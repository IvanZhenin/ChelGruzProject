   M S B A M L   `   `   `  ����D  @GruzChelb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GruzChelb.PagesGruzChelb.Pages  5      !    GruzChelb.Pages.ClientsList   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:GruzChelb.Pages     5      -   5          N� Title$  ClientsList��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��420* �6   �� 5      $��55 �6   .+�5   
   �� $��0��6   
�������$��3��64   
���   �$��3q�5       �� CornerRadius$ 15��6%   �� 5   
   $��0��6   $#��Список клиентов��6!      blockAddHeader#��[  68   $��Center=�5      $��Center=�61   �� 5   
   $��	350 0 0 0q�6   
���   �$	��1.5q�68   $ 3��6N   $��0��5      $��0��6   $��Center=�6.   $��Center=�6K   ._�5      �� -   5        
��� ��� � ToolTip$, &Вернуться на главную��5   B   ��5      �� 5      $��/Resources/btnback.png��6   $��32��6<   $��30��6G   5      5      �� 5   
   $��1��6   
���   �$��3q�62   
�������._�5       &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridClients��5          AutoGenerateColumns .  
������    
IsReadOnly .    CanUserResizeRows .     CanUserReorderColumns .  	   SelectionMode$	 Single=�5!   a   
   Columns
 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Номер��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 5$   )      MinWidth$ 70Q�64      
FontFamily$ Verdana)�6B      FontSize$ 16Q�5%   )      
FontWeight$ Medium"�67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5#   8   �� *Id+5%   7     5&      $ Имя��6)   $	 140 5'   )   $	 140Q�65   $ Verdana)�6D   $ 16Q�5(   )   $ Medium"�67    5&   6   �� *Name+5(   7     5)      $ Фамилия��6)   $	 170 5*   )   $	 170Q�65   $ Verdana)�6D   $ 16Q�5+   )   $ Medium"�67    5)   :   �� *
SecondName+5+   7     5,      $ Отчество��6)   $	 170 5-   )   $	 170Q�65   $ Verdana)�6D   $ 16Q�5.   )   $ Medium"�67    5,   ;   �� *	ThirdName+5.   7     5/      $ Телефон��6)   $	 180 50   )   $	 180Q�65   $ Verdana)�6D   $ 16Q�51   )   $ Medium"�67    5/   :   �� *	Telephone+ �� StringFormat$
 +{0}��51   7     52      $ 
Адрес��6)   $	 180 53   )   $	 180Q�65   $ Verdana)�6D   $ 16Q�54   )   $ Medium"�67    52   8   �� *	Address+54   7   4  .System.Windows.Controls.DataGridTemplateColumn  55      $	 120 6-   $	 120Q�69      CellTemplate 56      �� 57      .R�58   "   �� -   5        $��Изменить��58   )   $��Verdana)�6Q   $��16(�59   )   $��Medium"�67   
��� ���6K   5:      5<      5>      5?      �� 5A   
   -   5        $��2��5A      $��Добавить��6   $
��Left=�61     buttonTransTwo#��[ 5B      $��15 0 0 0q�69   6K   �� 5C   
   -   5        $��2��5C      $��Удалить��6   $��Right=�60   #��[ 5D      $��0 0 15 0q�69   6K   5E      5F      