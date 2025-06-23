# NucleusWPF.Classic.MVVM

A lightweight MVVM utility library for WPF applications targeting .NET Framework.  
Provides essential services, dependency injection, and command implementations to streamline WPF development.

> **Note:** This library is a port of [NucleusWPF.MVVM](https://github.com/chris972009/NucleusWPF.MVVM) for .NET Framework 4.8.  
> It brings the same MVVM utilities to classic WPF applications targeting .NET Framework.

## Features

- **Dependency Injection**: Simple container for registering and resolving services and view models (singleton and transient).
- **Message Service**: Interface and implementation for displaying messages and exceptions to users.
- **Window Service**: Interface and implementation for mapping and displaying windows based on view models.
- **RelayCommand / AsyncRelayCommand**: Synchronous and asynchronous command implementations for use in MVVM.
- **ViewModelBase**: Base class implementing `INotifyPropertyChanged` for property change notifications.
- **ValidViewModel**: Base class implementing `INotifyDataErrorInfo` for validation scenarios.

### Prerequisites

- [NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) or later
- WPF project (Windows only)

### Installation

Clone or add the NucleusWPF.Classic.MVVM project to your solution:

## Usage
]
- [DependencyInjection](docs/DependencyInjection.md)
  - [Registering Services](docs/DependencyInjection.md#registering-services)
    - [Transient/Scoped Services](docs/DependencyInjection.md#transientscoped-services)
    - [Singleton Services](docs/DependencyInjection.md#singleton-services)
  - [Retrieving Services & ViewModels](docs/DependencyInjection.md#retrieving-services--viewmodels)

- [IWindowService](docs/IWindowService.md)
  - [Mapping ViewModels to Views](docs/IWindowService.md#mapping-viewmodels-to-views)
  - [Opening a Window](docs/IWindowService.md#opening-a-window)
    - [Show Method](docs/IWindowService.md#show-method)
    - [ShowDialog Method](docs/IWindowService.md#showdialog-method)

 API Docucumentation is available [here](https://chris972009.github.io/NucleusWPF.MVVM/)