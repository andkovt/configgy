# Configgy

Simple .NET configuration parsing library.

## Quick Start
Install base Package
```bash
dotnet add package AndKovt.Configgy
```

Install one or more source providers
```bash
dotnet add package AndKovt.Configgy.Yaml
```

Create a configuration class
```c#
using AndKovt.Configgy;

public class AppConfig : IConfig
{    
    public string StringField { get; set; }
    public int IntField { get; set; }
    public bool BoolField { get; set; }
}
```

Build the config instance
```c#
using AndKovt.Configgy;

var config = new ConfigBuilder<AppConfig>()
    .AddSource(new YamlSource("config.yml"))
    .Build();
```

Checkout Samples for more examples

## Features
- Supports parsing primitive types
- Supports parsing collections
- Supports nested configuration classes

### Supported Primitive Types
- string
- bool (true/false/0/1/yes/no)
- int
- double
- float
- long

### Supported Sources
- YAML