   M S B A M L   `   `   `  ����D  @GruzChelb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:GruzChelb	GruzChelb  5   	       GruzChelb.Menu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:GruzChelb     5   	     � Title$
  Menu��5      $	��650��6   $
��1050��6"    � WindowStyle$
 None=�6/    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FF2B4361��6    � Offset$ 0.002Q�6-   � 5      $ 	#FF959FAC��6   $ 1Q�6-   � 5      $ 	#FF3A82D7��6   $	 0.2Q�6-   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��950 �6   �� 5      $��70 �6   	 � RowDefinitions	 5   
   �� 5      $��95 �6   �� 5      $	��495 �6   �� 5      $��60 �6   .+�5   
   � -   5        $��toolBar��5      
������ $��2��67   $��3��6H   6{   �� 5   
   
 �� Orientation$
 
Horizontal=�6   $��Center=�6.   $��0��6K   $��2��6X   $��50��6h   .+�5      �� -   5        $��
collButton��5      $��15��6&   $��15��62   $��5q�6=   ��5      �� 5       �� 
TargetType" ��6   .�5       ��  �� Property � �� �� Value$ /Source/coll.png��63    �� Triggers 5!      �� 5"       �� Binding 6*   �� *IsMouseOver+ �� ElementName$ 
collButton�� �� Value True6b   .a�5#   "   ��  � ��$ /Source/coll2.png��6;   5$      5&       J� Effect 5(      -  'System.Windows.Media.Effects.BlurEffect  5)         Radius$ 1Q�6!   5+      �� 5,      -   5        $��closeButton��5,      $��15��6'   $��15��63   $��5q�6>   ��5-      �� 5.      " ��6   .�5/      ��  � ��$ /Source/close.png��63    50      �� 51       6*   �� *IsMouseOver+$ closeButton�� True6c   .a�52   "   ��  � ��$ /Source/close2.png��6;   53      55       57        58      $ 1Q�6!   5:      5;      �� 5<   
   -   5         ��Name$ topLogo��5<      $��/Source/logo.png��6!   $��1��6;   $	��135��6K   $	��134��6W   $��Center=�6d   $
��Left=�6   $��	102,0,0,0q�6�   �� 5=   
   $��0��6   $��1��6!   
�������$��TimesNewRoman)�6D   $��DemiBold"�6_   $��28(�6u    �� TextAlignment$ Center=�6�   $��294,30,254,25q�6�   Челябинск-груз6�   �� 5>   
   -   5         ��Name$ btnBack��5>      $��20(�6"   $��1��60   $��
Назад��6@   $
��Left=�6P   $��
741,34,0,0q�6k   $	��Top=�6   $��36��6�   $	��175��6�    J� RenderTransformOrigin$ 
0.67,0.501&�6�   �� 5?   
   $��2��6   $��Label��6    $
��Left=�60   $��	116,0,0,0q�6K   $��1��6^   $	��Top=�6k   $��26��6�   $��0��6�    � 5@   
   -   5        $��	MenuFrame��5@   [     � NavigationUIVisibility$ Hidden=�6   $��1��6l   $��1��6y   5A      5B      