projects repo for 
# Building Interactive Systems 2023 

## Project Tetris on DualPanto

This project implements a Tetris game for blind people on the DualPanto. 
Therfore the DualPanto gives haptical feedback to the user so that with the upper handle the falling block can be moved left and right and with the lower handle the shapes of the blocks on the ground can be recognized.

This is a fork of the private repository of the project group.

# Installation

1. Add clone this repository to your local machine
`git clone https://github.com/valteu/tetris-dual-panto.git`
2. Install [Unity Hub](https://unity3d.com/get-unity/download)
3. Install the DualPanto Toolkit:
Make sure to execute the following commands after cloning to add the [Dualpanto Toolkit](https://github.com/HassoPlattnerInstituteHCI/unity-dualpanto-framework)
as a submodule
```
cd path/to/repo
cd Assets
git submodule update --init --recursive
```
Note: The project can simulate the DualPanto without the actual hardware. However, the hardware is recommended for the best experience and the simulation software has slightly different functionalities.
