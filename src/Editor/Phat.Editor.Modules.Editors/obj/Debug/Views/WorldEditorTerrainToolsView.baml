   M S B A M L   `   `   `  ����V  RPhat.Editor.Modules.Editors, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:Phat.Editor.Modules.Editors.ValueConverters+Phat.Editor.Modules.Editors.ValueConverters  5      C    =Phat.Editor.Modules.Editors.Views.WorldEditorTerrainToolsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      Fvc9clr-namespace:Phat.Editor.Modules.Editors.ValueConverters    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5
      D   >Phat.Editor.Modules.Editors.ValueConverters.FlipValueConverter   FlipValueConverter��%   &	          5   
   6    5       J� InputBindings 5      �� 5   
   
 �� Key$ P��6    �� CommandParameter$ Paint��6@    �� Command 6   �� *SetToolCommand+6@   �� 5   
   $ F��6   $
 Fill��6@    6   �� *SetToolCommand+6@   �� 5   
   $ E��6   $ 
EyeDropper��6@    6   �� *SetToolCommand+6@   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �  � Orientation$ 
Horizontal=�6   .+�5      	� $��Paint��6    	� 	GroupName$ Tools��6*   $��5q�6<   $��Paint��6j   ��6G   �� *SetToolCommand+ d� 	IsChecked 6�   �� *IsPaintSelected+	 �� Mode$	 OneWay=�	� 5      $
��Fill��6   $ Tools��6)   $��5q�6;   $
��Fill��6i   ��6F   �� *SetToolCommand+ 6�   �� *IsFillSelected+$	 OneWay=�	� 5      $��Eye Dropper��6   $ Tools��60   $��5q�6B   $��
EyeDropper��6p   ��6M   �� *SetToolCommand+ 6�   �� *IsEyeDropperSelected+$	 OneWay=��� 5      $��Show Grid Lines��6   $��5q�61    6<   �� *IsGridVisible+$	 TwoWay=�5      �� 5!   
   $��1��5"      $��Disabled=�5%      ��5#      �� *Tiles+
 �� SelectedItem
 5$      �� *SelectedTile+��5'      �� 5(      .R�5)      �� $��32��6   $��32��6(   $��5q�63   $��Uniform=�6d    J� RenderTransformOrigin$ 0.5, 0.5&�6v   ��6>   �� *Sprite.SpriteImage+z�5*      �� 5+       �� ScaleX 6-   �� *Sprite.IsFlippedHorizontally+ �� 	Converter# [   �� ScaleY 6�   �� *Sprite.IsFlippedVertically+# [  5-      5.      ��51      �� 52      .R�53      � $ 
Horizontal=�6    54      56      57      58      