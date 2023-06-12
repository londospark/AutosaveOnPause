using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace AutosaveOnPause;

public abstract class Configuration<TConfig> where TConfig : class, new()
{
    private static TConfig instance;
        
    public static TConfig Load()
    {
        if (instance == null)
        {
            var configPath = GetConfigPath();
            var xmlSerializer = new XmlSerializer(typeof(TConfig));
            try
            {
                if (File.Exists(configPath))
                {
                    using var streamReader = new StreamReader(configPath);
                    instance = xmlSerializer.Deserialize(streamReader) as TConfig;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        return instance ??= new TConfig();
    }

    public static void Save()
    {
        if (instance is null) return;

        var configPath = GetConfigPath();

        var xmlSerializer = new XmlSerializer(typeof(TConfig));
        var noNamespaces = new XmlSerializerNamespaces();
        noNamespaces.Add("", "");
        try
        {
            using var streamWriter = new StreamWriter(configPath);
            xmlSerializer.Serialize(streamWriter, instance, noNamespaces);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private static string GetConfigPath()
    {
        return typeof(TConfig).GetCustomAttributes(typeof(ConfigurationPathAttribute), true)
                .FirstOrDefault() switch
            {
                ConfigurationPathAttribute configPathAttribute => configPathAttribute.Value,
                _ => $"{typeof(TConfig).Name}.xml"
            };
    }
}