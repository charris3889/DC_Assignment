   M S B A M L   `   `   `  ����F  BChatUserOne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ChatUserOneChatUserOne  5   	       ChatUserOne.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ChatUserOne     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5   
   �� -   5        $��MessagesListView��5   R   $��	130,0,0,0q�6>    �� View 5      �� 5      .P�5      �� $��Chatroom Name��6%    �� Width$	 120��6<   5      5      �� 5   
   -   5        $��MessageArea��5      $
��Left=�6%   $��130,401,0,0q�6@    �� TextWrapping$
 Wrap=�6U   $�� ��6i   $	��Top=�6q   $	��626��6�   $��33��6�   �� 5      -   5        $��
SendButton��5      $
��Send��6+   $
��Left=�6:   $��756,401,0,0q�6U   $	��Top=�6j   $��44��6�   $��33��6�   6�   �� 5   
   -   5        $��UsernameBox��5      $
��Left=�6%   $
 Wrap=�6@   $��Username��6T   $	��Top=�6d   $	��130��6|   $��22��6�   �� 5   
   -   5        $��CurrentChatUsersList��5   Q   $��
686,0,0,33q�6=   $	��112��6m   �� 5      -   5        $��ChatsListView��5   W   $��0,22,670,33q�6B    5      �� 5      .P�5      �� 5      5         ChatUserOne.LoginControl1  5      -   5          Name$ loginControl��5   "   $��Center=�68   $	��434��6S   $	��799��6`   5       5!      