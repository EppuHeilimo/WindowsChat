   M S B A M L   `   `   `  ����F  BWindowsChat, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	       WindowsChat.ConnectWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	     � Title$  ConnectWindow��5   	   $	��160��6   $	��300��6,   $	��300��68   $	��160��6G   $	��300��6W   $	��160��6f   .��5	      � $��3,3,3,3q�6    � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      $��5 �6   �� 5      $��25 �6   �� 5      $��5 �6   �� 5      $��25 �6   �� 5      $��5 �6   �� 5      $��25 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� .��6   IP address:6"   �� 5      -   5         ��Name$ ipBox��5      $	��120��6%    �� 	MaxLength$ 20��64   $	��120��6C   $��	127.0.0.1��6R   6e   �� 5      .��6   Port:6   �� 5      -   5        $ portBox��5      $ 5��6'   $��60��65   $
��3000��6C   6Q   5      �� 5   
   $ 
Horizontal=�6   $��2��6.   .+�5      �� .��6   Channel name:6#   �� 5      -   5        $ 
channelBox��5      $	��190��6*   $	��190��69   $ 20��6H   $��Default��6W   6h   5      �� 5   
   $��4��6   $ 
Horizontal=�6"   .+�5      �� .��6   Your nickname:6%   �� 5      -   5        $ nickBox��5      $	��185��6'   $	��185��66   $ 16��6E   $
��User��6T   6b   5       �� 5!   
   $ 
Horizontal=�6   $��6��6.   .+�5"      �� -   5         ��Name$ 
BtnConnect��5"      $��	55,0,10,0q�6)   .��6W   	Connect6`   �� 5#      -   5        $ BtnNewServer��5#      $��0,0,0,0q�6+   .��6Y   
New Server6e   �� 5$      -   5        $ 	BtnCancel��5$      $��10,0,0,0q�6(   .��6T   Cancel6\   5&      5'      5(      