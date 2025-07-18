> [!WARNING]
> WORK IN PROGRESS!!!


> [!TIP]
> <a href="/Documentation~/UserManual.pdf">See this file in PDF</a>

<h1 align="center">User Manual</h1>

### Contents
1. [Introduction](#introduction)
   - [Overview](#overview)
   - [Features](#features)
   - [Requirements](#requirements)
2. [Getting Started](#getting-started)
   - [Installation](#installation)
   - [Basic Setup](#basic-setup)
   - [Quick Start Guide](#quick-start-guide)
3. [Package Structure](#package-structure)
   - [Assets](#assets)
   - [Scripts](#scripts)
   - [Settings](#settings)
4. [Usage](#usage)
   - [Core Concepts](#core-concepts)
   - [Best Practices](#best-practices)
   - [Examples](#examples)
5. [Troubleshooting](#troubleshooting)
   - [Common Issues](#common-issues)
   - [FAQ](#faq)
6. [Additional Resources](#additional-resources)
   - [API Reference](#api-reference)
   - [Tutorials](#tutorials)

---
## Introduction
### Overview
Custom Logger is a Unity development tool that provides a powerful and flexible logging system for Unity projects. It allows developers to centralize and manage console logging through configurable settings, making it easier to track and debug issues in their applications.

### Features
[Features content will go here]

### Requirements
- .NET 4.x Runtime
- ScriptableObject support

## Getting Started
### Installation
1. Open Unity Editor
2. Go to Window -> Package Manager
3. Click on the "+" button at the top left corner
4. Select "Add package from git URL"
5. Enter: `https://github.com/CRE-Tools/CustomLogger.git`
6. Click "Add"
7. Wait for the package to be installed

### Basic Setup
1. Open the Custom Logger Settings through Edit -> Project Settings -> Custom Logger (This will automatically create a new Settings file if it doesn't exist)
2. Add a new configuration to the list 
   - Assign a unique Key Name for the configuration respecting format (only letters, no spaces, no reserved characters)
   - Configure the desired log color
   - Set the initial state (enabled/disabled)
3. Save the settings by clicking on the "Apply Settings" button
4. Wait for recompile
5. Now settings can be accessed and edited through the file "Assets/LoggerSettings/Loggers.asset" (if it doesn't exist, follow step 1)

### Quick Start Guide
Here's a quick example of how to use Custom Logger in your code:

Using Custom Logger
- Use namespace PUCPR.CustomLogger
- Use Custom.Logger.DebugLog method to log messages passing: LogType (Log, Warning, Error), "this" as the object that is logging, the message and a key from CustomLoggerKey.AlwaysLog (this key is a default key that is always enabled)

```csharp
using PUCPR.CustomLogger;

public class ExampleClass : MonoBehaviour
{
    private void Start()
    {
        CustomLogger.DebugLog(LogType.Log, this, "Game started", CustomLoggerKey.AlwaysLog);
    }
}
```

Key points:
- Always use a unique key for each logging configuration
- The key must respect format: only letters, no spaces, no reserved strings
- Messages will only appear if the configuration for this key is enabled in settings

## Package Structure
### Assets
-Assets/LoggerSettings/
  - Loggers.asset (this is the settings file where you can configure the logging keys. will be created automatically when first opening the settings window)
-Package/CustomLogger/
  - Documentation~/
    - UserManual.md (this is the user manual)
    - UserManual.pdf (this is the user manual in pdf format)

### Scripts
-Package/CustomLogger/Scripts/
  - Runtime/
    - CodeGenerator.cs (this is the code generator that generates the CustomLoggerKey enum)
    - CustomLogger.cs (this is the main class for logging where the DebugLog method is)
    - CustomLoggerType.cs (this is object type that configurations for keys are stored in the settings file)
    - CustomLoggerKey.cs (auto generated enum with all the keys configured in the settings file)
    - CustomLoggerSettings.cs (this is the settings manager that handles the settings file)
  - Editor/
    - EDITOR_Logger.cs (Custom Editor for the settings file)

### Settings
- `Assets/LoggerSettings/Loggers.asset`: Contains all configurable settings per configuration key including:
  - Enable/Disable key
  - Color settings

## Usage
### Core Concepts
1. **Logging Configurations**
   - Each configuration is identified by a unique key
   - Configurations can be enabled/disabled in the settings file
   - Each configuration can have its own color settings

2. **Log Types**
Follow standard log types:
   - Log: Standard information messages
   - Warning: Warning messages
   - Error: Error messages

3. **Key System**
   - Each enum value represents a different logging configuration
   - Use these keys to organize and filter your logs

### Best Practices
[Best practices content will go here]

### Examples
[Examples content will go here]

## Troubleshooting
### Common Issues
1. **Logs not appearing**
   - Check if the configuration is enabled in settings
   - Verify the correct key is being used

2. **Color not applying correctly**
   - Check if the color is properly configured in settings
   - Verify the correct key is being used

### FAQ
1. **Can I use multiple configurations in one log?**
   - No, each log must belong to a single configuration
   - Use descriptive configurations to organize your logs

2. **How do I change log colors?**
   - Open the Custom Logger settings window
   - Select the configuration
   - Adjust the color settings as needed

3. **How do I disable all logs?**
   - Open the settings window
   - Disable all configurations

### Tutorials
[Tutorials content will go here]
