   M S B A M L   `   `   `  ����V  RPhat.Editor.Modules.Editors, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Phat.Editor.Modules.Editors.ViewModels&Phat.Editor.Modules.Editors.ViewModels  5      i9clr-namespace:Phat.Editor.Modules.Editors.ValueConverters+Phat.Editor.Modules.Editors.ValueConverters  5      :    4Phat.Editor.Modules.Editors.Views.FrameSetEditorView   5      ;vm4clr-namespace:Phat.Editor.Modules.Editors.ViewModels  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      Fvc9clr-namespace:Phat.Editor.Modules.Editors.ValueConverters    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5      D   >Phat.Editor.Modules.Editors.ValueConverters.FlipValueConverter   FlipValueConverter��%   &	          5   
   6    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   .+�5   
   �� $��20��5      $��5q�5      $��Disabled=�5      ��5      �� *Frames+ �� SelectedItem 5      �� *SelectedFrame+ �� Mode$ TwoWay=� J� InputBindings 5      �� 5      
 �� Key$ Delete��6    �� Command 6*   �� *DeleteFrame+��5"      �� 5#      .R�5$      � 5%      ��5(      �� 5)      .R�5*      �� $
��None=�5-       J� RenderTransformOrigin$ 0.5, 0.5&�5.      ��5,      �� *SpriteImage+z�5/      �� 50       �� ScaleX 6-   �� *IsFlippedHorizontally+	 �� 	Converter#	 [  
 �� ScaleY
 51   -   �� *IsFlippedVertically+#	 [  53      54      56      �� 58   
   $��1��59       �� Orientation$ 
Horizontal=�5:      .+�5<      �� $��1��6   $	��200��5=      $��5q�5>      $��Center=�5?      ��5@      �� *	Sprites+ 5A      �� *SelectedSprite+$ TwoWay=���5B      �� 5C      .R�5D      �� ��6$   �� *Key+5E      5G      �� 5I      $��Center=�5J      $
��Left=�5K      $��5q�5L      $��+��5N      ��5M      �� *
AddFrame+5N      5O      �� 5Q   
   $��2��5R      $
��None=�5U      $ 0.5, 0.5&�5V       � DataContext 5S      �� *	Animation+��5T      �� *SpriteImage+z�5W      �� 5X       6!   �� *IsFlippedHorizontally+#	 [  
 5Y   -   �� *IsFlippedVertically+#	 [  5[      �� 5]   
   $��3��6    � Minimum$
 0.05Q�6    � Maximum$ 2Q�6-    � Value 69   �� *FrameDuration+5^      5_      