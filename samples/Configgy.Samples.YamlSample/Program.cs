

using Configgy;
using Configgy.Samples.YamlSample;
using Configgy.Yaml;

var config = new ConfigBuilder<AppConfig>()
    .AddSource(new YamlSource("config.yml"))
    .Build();