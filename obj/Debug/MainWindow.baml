   M S B A M L   `   `   `  ����F  BWindowsChat, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	       WindowsChat.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	   -   5          � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5
         TabItem.Selected.Background �� Color  TabItem.Selected.Border  TabControlStyle1 �� 
TargetType �� Property �� Value �� 
TargetType �Name J� SnapsToDevicePixels-  'System.Windows.Input.KeyboardNavigation	   TabNavigation
 � RowDefinitions ��Name ]�Name K� IsItemsHost   TabIndex K� ZIndex ��Name   DirectionalNavigation ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 !     &	 B     7   7  �� 5   
   $ #FFFFFF��6>   �� 5   
   $ #ACACAC��6:   �� 5   
   " ��6)   .�5      ��  � ��$ 2q�6(   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��8   6+   �� 5       � ��8  6,   �� 5       � ��$ 1q�60   �� 5       � ��# � ��6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      � $ templateRoot��6   }�.  . $	 Local=�6d   
 5      �� 5   "   $ RowDefinition0��60   $
��Auto �6H   �� 5   "   $ RowDefinition1��60   $��* �6H   .+�5      ]� $ headerPanel��6(   
������ $��0��6V    . $��0,0,0,0q�6y   $��0��6�   $ 1��6�   $ 1��6�   �� 5      $ contentPanel��6%   #��z��6;   #��z��6g   #��z��6�   $��0��6�   $ 	Contained=�6�   $��1��6
  $ 2��6  $	 Local=�67  ._�5    "   �� $ PART_SelectedContentHost��63   $��SelectedContent��6U   #��z��6u   # z 6�   5!      5"       5#      P� 5$       � {� .  . �5%   "   �� $ templateRoot��6K   # � ��6e    � ��5&      5(      5*      5+      5,      .��5-      � $��
3, 0, 3, 3q�6   
 5.   
   �� 5/      $��23 �6   �� 50      $��* �6   �� 51      $��30 �6   .+�53   
   o� $��0��6   $��20��6   ��54      �� 55      .R�56      ]� $��Stretch=�6    57      .@�59      m� $
��File��6   .@�5:      m� -   5        $
��Exit��5:      6)   5;      m� 5<      $��Bookmark��6   .@�5=      m� $��Add server to bookmarks��6   m� 5>      $��Modify bookmarks��6   5?      m� 5@      -   5        $��Settings��5@      5A      m� 5B      $��History��6   5C      m� 5D      $
��Help��6   .@�5E      m� $��About��6   5F      m� 5G      $
��User��6   $��Right=�6%   .@�5H      m� $��Settings��6   5I      5J      �� 5K   
   -   5         ��Name$ TabContainer��5K      $��0,3,0,0q�6+   $��1��6<   .@�5L      �� $��Default Channel��6   .��5M      � ���	#FFE5E5E5.+�5N      �� -   5         ��Name$ Scroll��5N   #   .��5O      �� -   5         ��Name$ ChannelTextBlock��5O   $   $��Stretch=�6>   $
��Wrap=�6\   5P      5Q      5R      �� 5S      $��20��6   $��-3,0,0,0q�6$   ��5T      �� 5U      .R�5V      ��  ��Name$ BtnAdd��6!   -   5        $��8(�5V   H   .��6U   +6X   5W      5Y      5Z      � 5[   
   $��2��6    � ColumnDefinitions 5\      �� 5]      $��* �6#   �� 5^      $��75 �6#   .+�5`      �� -   5         ��Name$ 
MessageBox��5`      $��0,0,0,0q�6*    �� TextWrapping$
 Wrap=�6;   $�� ��6O   $��20��6W   $��25��6f   $��2��6u   $��Stretch=�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�    �� AcceptsReturn . $��0��6�     �� 	MaxLength$	  250��6�   6�   �� 5a      -	   5        $ BtnSend��5a      $
��Send��6>   $��75��6M   $��25��6[   $��2��6j   $��Right=�6w   $��1��6�   $��
0, 0, 0, 0q�6�   5b      5d      5e      