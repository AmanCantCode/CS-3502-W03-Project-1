# CS-3502-W03-Project-1-Multi-Threaded-Programming-and-IPC

This is two part Operating Systems project where Project A demonstrates multi-threading solution and Project B demonstrates inter process communication.

The following instructions are to be followed on a Linux based environment.

    Prerequisites:
        .NET SDK 8.0
        Git - To clone the repository
    
**If Prerequisites not met, open the terminal and use these commands**

    1. To install .NET SDK:
        sudo spt-get update
        sudo apt-get install -y dotnet-sdk-8.0
        sudo apt-get install -y dotnet-runtime-8.0
        dotnet –version (to verify installation)
        
    2. To install Git:
        sudo apt install git
        git --version (to verify installation)
        

-------------------------------------------------------------------
To run the projects:

Open the path to where the project is saved using the TERMINAL
    cd ~/Downloads/OS_Project1 (example)
    
    1. To execute Project A, run these commands in order:
        cd Project A
        cd MultiThread
        dotnet build
        dotnet run
    
    2. To test Project A, run these commands in order:
        cd - (To go back to Project A directory)
        cd Multi.Tests
        dotnet test
    
    **OPTIONAL**
    3. To run Phase 3 of Project A by itself, run these commands:
        cd - (To go back to Project A directory)
        cd Phase3
        dotnet build
        dotnet run
    
-------------------------------------------------------------------
    1. To execute Project B, open two TERMINAL windows:
    
    2. In window one open the path to Producer file.
        cd ~/Downloads/OS_Project1/ProjectB/Producer
        
    3. In window two open the path to Consumer file.
        cd ~/Downloads/OS_Project1/ProjectB/Producer
        
    4. In each TERMINAL window, run the commands:
        dotnet build 
        dotnet command
        
    5. To test Project B, go to the path for Project B 
    and go to IpcTests directory (either using one of the
    previously occupied terminal window or open a new terminal
    window):
        cd ~/Downloads/OS_Project1/ProjectB/IpcTests (example, to
        get to IpcTests with new terminal window)
        dotnet test
