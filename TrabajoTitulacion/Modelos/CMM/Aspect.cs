using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoTitulacion.Modelos.CMM
{
    public class Aspect
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "properties")]
        public List<Property> Properties { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "parentName")]
        public string ParentName { get; set; }
        [JsonProperty(PropertyName = "prefixedName")]
        public string PrefixedName { get; set; }
        //Nota: Showable no pertenece al sistema, agregada por el desarrollador
        [JsonIgnore]
        public bool Showable { get; set; }
        //Nota: ModeloPerteneciente no pertenece al sistema, agregada por el desarrollador
        [JsonIgnore]
        public Model ModeloPerteneciente { get; set; }        

        public Aspect()
        {
            ModeloPerteneciente = new Model();
        }

        public void Audio()
        {
            Name = "audio:audio";
            Title = "Audio";
            Showable = true;
            Properties = new List<Property>();
            Properties.Add(new Property("audio:album","Album","d:text"));
            Properties.Add(new Property("audio:artist", "Artist", "d:text"));
            Properties.Add(new Property("audio:composer", "Composer", "d:text"));
            Properties.Add(new Property("audio:engineer", "Engineer", "d:text"));
            Properties.Add(new Property("audio:genre", "Genre", "d:text"));
            Properties.Add(new Property("audio:trackNumber", "Track Number", "d:int"));
            Properties.Add(new Property("audio:releaseDate", "Release Date", "d:date"));
            Properties.Add(new Property("audio:sampleRate", "Sample Rate", "d:int"));            
            Properties.Add(new Property("audio:sampleType",
                "Sample Type", "Audio Sample Type, typically one of 8Int, 16Int, " +
                "32Int or 32Float", "d:text"));
            Properties.Add(new Property("audio:channelType", "Channel Type", "Audio " +
                "Channel Type, typically one of Mono, Stereo, 5.1 or 7.1", "d:text"));
            Properties.Add(new Property("audio:compressor", "Compressor","" +
                "Audio Compressor Used, such as MP3 or FLAC", "d:text"));
        }

        public void Versionable()
        {
            Name = "cm:versionable";
            Title = "Versionable";
            Showable = false;
            Properties = new List<Property>();
            Properties.Add(new Property("cm:versionLabel", "VersionLabel","d:text"));
            Properties.Add(new Property("cm:versionType", "Version Type", "d:text"));
            Properties.Add(new Property("cm:initialVersion", "Initial Version", "d:boolean"));
            Properties.Add(new Property("cm:autoVersion", "Auto Version", "d:boolean"));
            Properties.Add(new Property("cm:autoVersionOnUpdateProps", "Auto Version - on update " +
                "Properties only", "d:boolean"));
        }

        public void Titled()
        {
            Name = "cm:titled";
            Title = "Titled";
            Showable = true;
            Properties = new List<Property>();
            Properties.Add(new Property("cm:title", "Title", "d:mltext"));
            Properties.Add(new Property("cm:description", "Description", "d:mltext"));
        }
        public void Auditable()
        {
            Name = "cm:auditable";
            Title = "Auditable";
            Showable = false;
            Properties = new List<Property>();
            Properties.Add(new Property("cm:created", "Created", "d:datetime"));
            Properties.Add(new Property("cm:creator", "Creator", "d:text"));
            Properties.Add(new Property("cm:modified", "Modified", "d:datetime"));
            Properties.Add(new Property("cm:modifier", "Modifier", "d:text"));
            Properties.Add(new Property("cm:accessed", "Accessed", "d:datetime"));
        }
        public void Author()
        {
            Name = "cm:author";
            Title = "Author";
            Showable = true;
            Properties = new List<Property>();
            Properties.Add(new Property("cm:author", "Author", "d:text"));
        }

        public static List<Aspect> Aspects()
        {
            List<Aspect> aspects = new List<Aspect>();
            Aspect audioAspect = new Aspect();
            audioAspect.Audio();
            aspects.Add(audioAspect);
            Aspect versionableAspect = new Aspect();
            versionableAspect.Versionable();
            aspects.Add(versionableAspect);
            Aspect TitledAspect = new Aspect();
            TitledAspect.Titled();
            aspects.Add(TitledAspect);
            Aspect auditableAspect = new Aspect();
            auditableAspect.Auditable();
            aspects.Add(auditableAspect);
            Aspect authorAspect = new Aspect();
            authorAspect.Author();
            aspects.Add(authorAspect);
            return aspects;
        }
    }
}
