# AlMartsonUnityHandyToolsCompilation
My own custom made set of Unity3D Handy Tools: 

## 1- Copying Components:
In the Unity Editor, in the TAB named 'GamObjects and Components' you will have:
* Multiple Components Copy between two GameObjects.
* Components deletion
* GameObject Replace (by another GO in Scene or a Prefab): Transform (position + rotation + scale) and Components.

![Image of Copying Components](/Readme_Images/1_1_CopyAllComp_Recortada.png)


## 2- Baking Lightmaps: 
In the Unity Editor, in the TAB named 'MyCustomBake/Bake All Objects Including Emission (GameObjects) Light Into Lightmaps' you will have the option to:
* Bake Emissive Materials into Lightmaps (Note: The GOs must be marked as Static, and have a particular Layer or a Tag, as decribed in the Tool). The Heart of the Source Code of this handy algorithm was taken from: https://support.unity3d.com/hc/en-us/articles/214718843-My-Emissive-material-shader-does-not-appear-in-the-Lightmap- ).

![Image of Baking Lightmaps](/Readme_Images/2_1_2_MyCustomBake-EmissiveMaterialsStaticIntoLightmap_Recortada.png)


Some of these tools belong to several authors (although I changed them a little to make them more usable for my case), credited at the begining of each Script in the 'summary' comment.

Thanks to this and other Free Tools I was able to finish some interesting Projects.. I am very grateful, so I decided to share it with you.

It was tested in Unity 2017.1 & 2018.4.0f1.

Have a nice day.

*********************************************

MIT License

Copyright (c) 2019 AlMartson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
