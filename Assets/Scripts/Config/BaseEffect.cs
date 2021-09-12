// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto/baseEffect.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace BaseEffect {

  /// <summary>Holder for reflection information generated from proto/baseEffect.proto</summary>
  public static partial class BaseEffectReflection {

    #region Descriptor
    /// <summary>File descriptor for proto/baseEffect.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BaseEffectReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZwcm90by9iYXNlRWZmZWN0LnByb3RvEgpCYXNlRWZmZWN0InwKCkJhc2VF",
            "ZmZlY3QSQAoRYmFzZUVmZmVjdF9jb25maWcYASADKAsyJS5CYXNlRWZmZWN0",
            "LkJhc2VFZmZlY3QuQmFzZUVmZmVjdEl0ZW0aLAoOQmFzZUVmZmVjdEl0ZW0S",
            "CwoDa2V5GAEgASgJEg0KBXZhbHVlGAIgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::BaseEffect.BaseEffect), global::BaseEffect.BaseEffect.Parser, new[]{ "BaseEffectConfig" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::BaseEffect.BaseEffect.Types.BaseEffectItem), global::BaseEffect.BaseEffect.Types.BaseEffectItem.Parser, new[]{ "Key", "Value" }, null, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// @wrapper 基础效果表(BaseEffect.pb.json)
  /// </summary>
  public sealed partial class BaseEffect : pb::IMessage<BaseEffect> {
    private static readonly pb::MessageParser<BaseEffect> _parser = new pb::MessageParser<BaseEffect>(() => new BaseEffect());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BaseEffect> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::BaseEffect.BaseEffectReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BaseEffect() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BaseEffect(BaseEffect other) : this() {
      baseEffectConfig_ = other.baseEffectConfig_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BaseEffect Clone() {
      return new BaseEffect(this);
    }

    /// <summary>Field number for the "baseEffect_config" field.</summary>
    public const int BaseEffectConfigFieldNumber = 1;
    private static readonly pb::FieldCodec<global::BaseEffect.BaseEffect.Types.BaseEffectItem> _repeated_baseEffectConfig_codec
        = pb::FieldCodec.ForMessage(10, global::BaseEffect.BaseEffect.Types.BaseEffectItem.Parser);
    private readonly pbc::RepeatedField<global::BaseEffect.BaseEffect.Types.BaseEffectItem> baseEffectConfig_ = new pbc::RepeatedField<global::BaseEffect.BaseEffect.Types.BaseEffectItem>();
    /// <summary>
    /// @name 基础效果配置
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::BaseEffect.BaseEffect.Types.BaseEffectItem> BaseEffectConfig {
      get { return baseEffectConfig_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BaseEffect);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BaseEffect other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!baseEffectConfig_.Equals(other.baseEffectConfig_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= baseEffectConfig_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      baseEffectConfig_.WriteTo(output, _repeated_baseEffectConfig_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += baseEffectConfig_.CalculateSize(_repeated_baseEffectConfig_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BaseEffect other) {
      if (other == null) {
        return;
      }
      baseEffectConfig_.Add(other.baseEffectConfig_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            baseEffectConfig_.AddEntriesFrom(input, _repeated_baseEffectConfig_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the BaseEffect message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class BaseEffectItem : pb::IMessage<BaseEffectItem> {
        private static readonly pb::MessageParser<BaseEffectItem> _parser = new pb::MessageParser<BaseEffectItem>(() => new BaseEffectItem());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<BaseEffectItem> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::BaseEffect.BaseEffect.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public BaseEffectItem() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public BaseEffectItem(BaseEffectItem other) : this() {
          key_ = other.key_;
          value_ = other.value_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public BaseEffectItem Clone() {
          return new BaseEffectItem(this);
        }

        /// <summary>Field number for the "key" field.</summary>
        public const int KeyFieldNumber = 1;
        private string key_ = "";
        /// <summary>
        /// @name 基础效果编号
        /// @key
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Key {
          get { return key_; }
          set {
            key_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "value" field.</summary>
        public const int ValueFieldNumber = 2;
        private string value_ = "";
        /// <summary>
        /// @name 效果描述
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Value {
          get { return value_; }
          set {
            value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as BaseEffectItem);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(BaseEffectItem other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Key != other.Key) return false;
          if (Value != other.Value) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (Key.Length != 0) hash ^= Key.GetHashCode();
          if (Value.Length != 0) hash ^= Value.GetHashCode();
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
          if (Key.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(Key);
          }
          if (Value.Length != 0) {
            output.WriteRawTag(18);
            output.WriteString(Value);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (Key.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Key);
          }
          if (Value.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Value);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(BaseEffectItem other) {
          if (other == null) {
            return;
          }
          if (other.Key.Length != 0) {
            Key = other.Key;
          }
          if (other.Value.Length != 0) {
            Value = other.Value;
          }
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 10: {
                Key = input.ReadString();
                break;
              }
              case 18: {
                Value = input.ReadString();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
