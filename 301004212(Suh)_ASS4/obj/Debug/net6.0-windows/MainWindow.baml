   M S B A M L   `   `   `  ����N  J301004212(Suh)_ASS4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:_301004212_Suh__ASS4_301004212_Suh__ASS4  5   	   %    _301004212_Suh__ASS4.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:_301004212_Suh__ASS4     5   	     � Title$  HenrySuh_ASS#4��5   	   $	��300��6    $	��600��6-    � WindowStartupLocation$ CenterScreen=�69   .��5	      � $��Center=�6   $	��600��6(   $	��284��64   $	��Top=�6A    � RowDefinitions 5
   
   �� 5      $
��200* �6   �� 5      $
��100* �6   .+�5   
   �� -   5         ��Name$ CustomerList��5      ��6)   �� *
Customer+ �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 30��61    �� DisplayMemberBinding 6<   �� *Id+�� 5      $��Title��6%   $ 40��64    6?   �� *Title+�� 5      $��	FirstName��6%   $	 100��68    6D   �� *	FirstName+�� 5      $��LastName��6%   $	 100��67    6C   �� *
LastName+�� 5      $��Company��6%   $ 90��66    6A   �� *CompanyName+�� 5      $��Address��6%   $	 170��66    6B   �� *	Address+�� 5      $��Country��6%   $ 60��66    6A   �� *	Country+5      5      �� 5   
   -   5        $��1��5      $	��Add��6   $
��Left=�6,   $��	19,10,0,0q�6G   $	��Top=�6Z   $��95��6r   5      �� 5   
   -   5        $��1��5      $
��Edit��6   $
��Left=�6-   $��
134,10,0,0q�6H   $	��Top=�6\   $��95��6t   5      �� 5    
   -   5        $��1��5       $
��Find��6   $
��Left=�6-   $��	19,43,0,0q�6H   $	��Top=�6[   $��95��6s   5!      �� 5"   
   -   5        $��1��5"      $��Delete��6   $
��Left=�6/   $��
134,43,0,0q�6J   $	��Top=�6^   $��95��6v   5#      �� 5%   
   -   5        $��Sort by First Name��5%      $
��Left=�6.   $��
253,10,0,0q�6I   $��1��6]   $	��Top=�6j   $	��150��6�   5&      �� 5'   
   -   5        $��Sort by Company Name��5'      $
��Left=�60   $��
253,43,0,0q�6K   $��1��6_   $	��Top=�6l   $	��150��6�   5(      �� 5)   
   -   5        $��1��5)      $��Customers From Canada��6   $��
428,10,0,0q�6>   $	��Top=�5*   *   $
��Left=�6B   $	��150��6]   �� 5+   
   -	   5        $��1��5+      $��Generate Report��6   $��
428,43,0,0q�68   $	��Top=�5,   (   $
��Left=�6@   $	��150��6[   5-      5.      