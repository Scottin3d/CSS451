# CSS451
Introduces practical and popular three-dimensional (3-D) graphic algorithms. Examines modeling (how to build 3-D objects), animation (how to describe the motion of objects), and rendering (how to generate images of 3-D objects in animation).  

This class we will learn about the essence of 3D interactive applications including: user interface, virtual cameras and their manipulations, review of basic applied linear algebra, mesh and related data structures, hardware shading language, illumination model, texture mapping, and some foundation modeling techniques such as rotational sweeps. We will use Unity3D as the vehicle for learning these concepts. After this class, students are expected to understand the basic computer graphics terminology, concepts, algorithms, and be able to design and implement 3D interactive computer graphics related programs.  

**NOT GOALS** We are not here to learn DirectX, OpenGL, XNA, GLUT, FLTK, MFC (Microsoft Foundation Classes), WPF, Swing, WinForm, Java, C, C++, or Unity3D etc. These are all transient technologies!  

**GOALS** The primary goal of this class is to ensure that, given typical GUI and graphics API, students will be able to design and implement interactive applications based on real life user requirements.  

### Objectives  
The objectives of this course are for students to:  

- Study the Model-View-Controller (MVC) software architecture and its support for implementing interactive graphical applications  
- Understand the essential conceptual areas of 3D computer graphics: modeling, animation, and rendering  
	- Modeling: Coordinate transformation pipeline, basics of hierarchical modeling, mesh representation  
	- Animation: simple linear interpolation  
	- Rendering: illumination models  
- Learn the programming model of modern 3D graphical Application Programming Interface (API)  
	- Issues behind 3D API design  
	- Device initialization  
	- Vertex and Pixel shaders  
- Practice the graphics concepts learned based on a graphical API  

### Learning Outcomes  
Upon successful completion of the course, students shall be able to:  

- Discuss how MVC software architecture can support the implementation of 3D graphical applications.  
- Describe popular interactive graphical software systems (e.g., Microsoft  Powerpoint, or Adobe Photoshop) in the context of MVC architecture.  
- Discuss the programming model of contemporary graphical API (e.g., OpenGL, Direct3D, or XNA).  
- Design and build 3D interactive graphical applications that supports:  
	- Real-time user control and manipulation of graphical scenes  
	- Graphical scenes with multiple camera views of 3D models  
	- 3D models organized as scene nodes in scene graphs with multiple transformable components  
	- 3D models rendered by custom vertex and pixel shaders with basic effects including: Lambertian and Per-pixel Phong illumination based on simple light source types (e.g., point, directional, and spot-light).  

[Top](CSS451)  
	
## Jump To  
[MP1](mp-1)  
[MP2](mp-2)  

## MP 1  				  
### Repo  
https://github.com/Scottin3d/CSS451  

### Objective  
In this programming assignment, we familiarize with the learning tool, Unity, that we will be using for the rest of this quarter. You should also use this opportunity to:  

- Evaluate how comfortable you are with the kinds of learning that is expected [mainly on how to use Unity as a tool].  
- Ensure that you understand exactly what is involved in submitting a programming assignment.  

This assignment should take you no more than 5 or 6 hours to complete. Most of the time should be spent on asking and looking for answers to: “how do I make Unity do …”  

### Approach  
Initially, your world consists of a CreationPlane and a CreationTarget:

#### CreationPlane  
Is a Quad with:  
- Transform:
- Position: 0, 0, 0
- Rotation: 90, 0, 0 (such that the Quad is on the x/z plane)
- Scale: 14, 14, 14

Note: the corners of the plane spans between –7 to +7.  
If you choose to work with a Plane, your transform settings will be different, but, it is important to make sure the size and location of the CreationPlane to be specified as above.  

#### CreationTarget  
Is a sphere with:
- Transform:
- Position: user defined!
- Rotation: 0, 0, 0
- Scale: 0.5, 0.5, 0.5
- Color: black

#### Object Creation  
You must also support a drop down menu that provides the options of creating a Sphere, Cube, or Cylinder. 
Here are the specification for each of the shape:
- Sphere:
	- Size of 1 (scale = 1)
	- Speed = 1 unit per second
	- No rotation
	- Moving direction x-axis
	- Range: 0 < x < 5
	- Color:
	
	When traveling in the positive-X: color= (1, 1, 1).  
	When traveling in the negative-X: color=(0, 1, 1).  
	
- Cube:
	- Size of 1 (scale = 1)
	- Speed = 1 unit per second
	- Rotation speed = about y-axis, 90-degrees per second
	- Moving direction y-axis
	- Range: 0 < y < 5
	- Color:
	
	When traveling in the positive-Y: color=(1, 1, 1).  
	When traveling in the negative-Y: color=(1, 0, 1).  

- Cylinder:
	- Scale = (1, 2, 1)
	- Speed = 1 unit per second
	- No rotation
	- Moving direction z-axis
	- Range: 0 < z < 5
	- Color:

	When traveling in the positive-Z: color=(1, 1, 1).  
	When traveling in the negative-Z: color=(1, 1, 0).  
	
### Behaviors  
There are three basic behaviors that you must support.  

1. Shape creation:
	- When the user selects a shape-item from the drop down menu, you should create a new corresponding shape at the location defined by the CreationTarget. In all cases, a newly created shape should be resting on the CreationPlane and attempt to travel towards the position direction. If the creation position is larger than the valid range of 5, the created shape will then travel in the negative direction. After a shape’s first change of traveling direction, it should never cross the 0 to 5 range.
2. Creation target manipulation: 
	- You must support Left-Mouse-Button click (LMB)
	- When LMB is over the CreationPlane and not over any other objects, the CreationTarget is moved to the mouse click position. 
	- Note:  
	
	CreattionTarget must always be entirely above the CreationPlane.  
	CreattionTarget must always be entirely above the CreationPlane.  

3. Object deletion
	- LMB over a created object should delete the object. 
	- Note: 
	
	One can never delete the CreationPlane or the CreationTarget.  

## MP 2  
### Repo  
https://github.com/Scottin3d/CSS451/tree/MP2  

### Objective  
In this programming assignment, we want to familiarize with implementing solutions with GUI, and designing small scale interactive solutions based on what we can implement for a MVC architecture in the Unity environment.  

### Approach  
We will build a simple hierarchical editor, one where we can select, manipulate, and add new primitive GameObjects. Please refer to my attempt at the solution (download unzip to run) or WebGL Version of the solution (Links to an external site.).  

#### Objects  
Initially, your world will consist of a static plane (for friendly reference), GrandParent, Parent, and Child.  

- GrandParent:
	- Cube
	- blue-ish color
- Parent:
	- Sphere
	- Green-ish color
	- A child of GrandParent
- Child:
	- Cylinder
	- Red-ish color
	- A child of Parent
	
At any point, you can click the left mouse button (LMB) to select any of the GameObject defined in the scene. LMB clicking on the static plane or an empty space results in selecting nothing. Selected object must:  

- Be displayed in a unique color (mine is some kind of yellow-ish)  
- Be semi-transparent  
- Unselecting an object must cause it to return to its original color.  

You will create two simple GUI widget to support editing operations.  

#### XformControl  
This widget will allow the editing of the transform of the currently selected GameObject:  

- Name: name of the GameObject currently under editing.  
- Mode: mode of editing, either Translation, Scaling, or Rotation  
- Sliders: x/y/z slider for controlling the current mode  
	- You must use the same sliders to support all three transformation modes  
	- Sliders must show numeric echos  
	- Slider ranges must change according to the current selected transformation mode and must make sense, e.g., translation between -10 to +10, rotation between -180 to 180, and scaling between 1 to 5.  
	
At any point, you can create a new GameObject primitive as the child of the current selected GameObject with the CreateObject dropdown menu. The primitives you must support creating are: Cube, Sphere, and Cylinder. When creating a new object, let mSelected be the currently selected GameObject:  

- If mSelected is null  
	- Newly created GameObject will be in black  
- Else  
	- Newly created GameObject will:  
	- have mSelected as parent  
	- if mSelected has other children  
		- Have the same materials as the siblings sharing the same mSelected parent  
	- Else  
		- Be in White.  
		
	- Note:
	
	The above means, new root object will always be in black, and new leave objects will always be in white.  

For convenience, so that you can see the newly created objects, new object should be slight offset by transform.postition + (1, 1, 1) of the currently selected object. When mSelected is null, I create new objects at (1, 1, 1). For the purpose of avoiding overlap, you can assume the parent’s scale is (1, 1, 1).  

There you have it, with the above, you have a functional but quite un-usable editor that is good for really not much. You do not need to support object deletion (but should be trivial).  

[Top](CSS451)  
