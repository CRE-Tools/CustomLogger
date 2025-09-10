## [1.0.0] - 2025-08-18

### Added
- imges from UserManual documentation
- UserManual pdf format

### Removed
- Deprecated code and documentation references

## [0.3.0] - 2025-07-21

### Added
- `InterceptingLogHandler.cs` - Custom log handler that intercepts and processes log messages
- `InterceptiorInitializer.cs` - Initializes the intercepting log handler

### Changed
- `CustomLoggerSettings.cs` - GetLoggerTypeSettings() is now public for InterceptingLogHandler access
- `CustomLogger.cs` - Obsolete. Will be removed in the next version


## [0.2.0] - 2025-07-18

### Added
- Scripts/Editor:
  - `EDITOR_Logger.cs` - Custom Editor for CustomLoggerSettings.cs
- Scripts/Runtime:
  - `CodeGenerator.cs` - Code generation utility with Enum generation support
  - `CustomLogger.cs` - Main logger implementation
  - `CustomLoggerKey.cs` - Generated enum for logger keys
  - `CustomLoggerSettings.cs` - Settings configuration
  - `CustomLoggerType.cs` - Logger type definition


## [0.1.0] - 2025-07-17

### Added
- This CHANGELOG
- README
- LICENSE
- package.json
  - Unity package config
