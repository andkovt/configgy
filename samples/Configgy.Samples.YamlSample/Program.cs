using AndKovt.Configgy;
using AndKovt.Configgy.Samples.YamlSample;
using AndKovt.Configgy.Yaml;

var config = new ConfigBuilder<AppConfig>()
    .AddSource(new YamlSource("config.yml"))
    .Build();

var tes = config.ObjectArray; 

Console.WriteLine(tes);