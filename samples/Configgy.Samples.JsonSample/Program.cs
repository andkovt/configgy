using AndKovt.Configgy;
using AndKovt.Configgy.Json;
using AndKovt.Configgy.Samples.YamlSample;

var config = new ConfigBuilder<AppConfig>()
    .AddSource(new JsonSource("config.yml"))
    .Build();

var tes = config.ObjectArray; 

Console.WriteLine(tes);