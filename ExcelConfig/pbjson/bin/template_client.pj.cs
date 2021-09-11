using System.Collections.Generic;

namespace {{packageInfo.packageNames.unique}}
{
    using pb = Google.Protobuf;

{{#each wrapperMessageInfos}}
    public class {{wrapperMessageNames.origin}}Loader : BaseConfigLoader<{{wrapperMessageNames.origin}}>
    {
        public override string ConfigName => "ConfigAssets/PbJson/{{wrapperPaths.[0].jsonPath}}";
        private {{wrapperMessageNames.origin}}Loader() {}
        private static readonly {{wrapperMessageNames.origin}}Loader s_instance = new {{wrapperMessageNames.origin}}Loader();

        public static {{wrapperMessageNames.origin}}Loader Instance
        {
            get
            {
                if (s_instance.Table == null)
                {
                    s_instance.Load();
                }

                return s_instance;
            }
        }

        {{#with wrapperMessageTypeInfo}}
        {{#each allMessageFields}}
        {{#if (eq typeInfo.isRepeated false)}}
        public {{typeInfo.typeNames.typePart}} {{fieldNames.pascal}} => Table.{{fieldNames.pascal}};
        {{/if}}{{! typeInfo.isRepeated}}
        {{/each}}{{! allFields}}
        {{/with}}{{! wrapperMessageTypeInfo}}

        {{#each mapHandledFields}}
        {{#with mapHandledFieldTypeInfo}}
        public IReadOnlyList<{{typeNames.typePart}}> {{mapHandledFieldNames.pascal}} => Table.{{mapHandledFieldNames.pascal}};
        {{#if (eq keyMessageFields.length 1)}}
        public IReadOnlyDictionary<{{#each keyMessageFields.[0].typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}, {{typeNames.typePart}}> {{typeNames.origin}}Dic => Table.{{typeNames.origin}}Dic;
        {{else}}
        public IReadOnlyDictionary<({{#each keyMessageFields}}{{#each typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}{{#unless @last}}, {{/unless}}{{/each}}), {{typeNames.typePart}}> {{typeNames.origin}}Dic => Table.{{typeNames.origin}}Dic;
        {{/if}}{{! (eq keyMessageFields.length 1)}}

        {{#if (eq keyMessageFields.length 1)}}
        public {{typeNames.typePart}} Find{{typeNames.origin}}({{#each keyMessageFields.[0].typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}} key)
        {{else}}
        public {{typeNames.typePart}} Find{{typeNames.origin}}(({{#each keyMessageFields}}{{#each typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}{{#unless @last}}, {{/unless}}{{/each}}) key)
        {{/if}}{{! (eq keyMessageFields.length 1)}}
        {
            {{typeNames.origin}}Dic.TryGetValue(key, out var value);
            return value;
        }

        {{/with}}{{! mapHandledFieldTypeInfo}}
        {{/each}}{{! mapHandledFields}}
    }

    public partial class {{wrapperMessageNames.origin}} : Pbjson.IRepeatedFieldConvert
    {
        {{#each mapHandledFields}}
        {{#with mapHandledFieldTypeInfo}}
        {{#if (eq keyMessageFields.length 1)}}
        public readonly Dictionary<{{#each keyMessageFields.[0].typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}, {{typeNames.typePart}}> {{typeNames.origin}}Dic = new Dictionary<{{#each keyMessageFields.[0].typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}, {{typeNames.typePart}}>();
        {{else}}
        public readonly Dictionary<({{#each keyMessageFields}}{{#each typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}{{#unless @last}}, {{/unless}}{{/each}}), {{typeNames.typePart}}> {{typeNames.origin}}Dic = new Dictionary<({{#each keyMessageFields}}{{#each typeInfo.typeNames.fullItems}}{{this}}{{#unless @last}}.{{/unless}}{{/each}}{{#unless @last}}, {{/unless}}{{/each}}), {{typeNames.typePart}}>();
        {{/if}}{{! (eq keyMessageFields.length 1)}}
        {{/with}}{{! mapHandledFieldTypeInfo}}
        {{/each}}{{! mapHandledFields}}

        public void RepeatedFieldToDictionary()
        {
            {{#each mapHandledFields}}
            {{#with mapHandledFieldTypeInfo}}
            foreach (var item in {{mapHandledFieldNames.pascal}})
            {
                {{#if (eq keyMessageFields.length 1)}}
                {{typeNames.origin}}Dic[{{#each mapHandledFieldTypeInfo.keyMessageFields}}item.{{fieldNames.pascal}}{{#unless @last}}, {{/unless}}{{/each}}] = item;
                {{else}}
                {{typeNames.origin}}Dic[({{#each mapHandledFieldTypeInfo.keyMessageFields}}item.{{fieldNames.pascal}}{{#unless @last}}, {{/unless}}{{/each}})] = item;
                {{/if}}{{! (eq keyMessageFields.length 1)}}
            }
            {{/with}}{{! mapHandledFieldTypeInfo}}
            {{/each}}{{! mapHandledFields}}
        }
    }
{{/each}}{{! wrapperMessageInfos}}

}