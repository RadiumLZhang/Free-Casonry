// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto/Condition.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Condition {

  /// <summary>Holder for reflection information generated from proto/Condition.proto</summary>
  public static partial class ConditionReflection {

    #region Descriptor
    /// <summary>File descriptor for proto/Condition.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ConditionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVwcm90by9Db25kaXRpb24ucHJvdG8SCUNvbmRpdGlvbiKXAQoJQ29uZGl0",
            "aW9uEjwKEGNvbmRpdGlvbl9jb25maWcYASADKAsyIi5Db25kaXRpb24uQ29u",
            "ZGl0aW9uLkNvbmRpdGlvbkl0ZW0aTAoNQ29uZGl0aW9uSXRlbRITCgtjb25k",
            "aXRpb25JZBgBIAEoAxIXCg9iYXNlQ29uZGl0aW9uSWQYAiABKAMSDQoFcGFy",
            "YXMYAyADKAViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Condition.Condition), global::Condition.Condition.Parser, new[]{ "ConditionConfig" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Condition.Condition.Types.ConditionItem), global::Condition.Condition.Types.ConditionItem.Parser, new[]{ "ConditionId", "BaseConditionId", "Paras" }, null, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// @wrapper 应用条件表(Condition.pb.json)
  /// </summary>
  public sealed partial class Condition : pb::IMessage<Condition> {
    private static readonly pb::MessageParser<Condition> _parser = new pb::MessageParser<Condition>(() => new Condition());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Condition> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Condition.ConditionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Condition() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Condition(Condition other) : this() {
      conditionConfig_ = other.conditionConfig_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Condition Clone() {
      return new Condition(this);
    }

    /// <summary>Field number for the "condition_config" field.</summary>
    public const int ConditionConfigFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Condition.Condition.Types.ConditionItem> _repeated_conditionConfig_codec
        = pb::FieldCodec.ForMessage(10, global::Condition.Condition.Types.ConditionItem.Parser);
    private readonly pbc::RepeatedField<global::Condition.Condition.Types.ConditionItem> conditionConfig_ = new pbc::RepeatedField<global::Condition.Condition.Types.ConditionItem>();
    /// <summary>
    ///@name 应用条件表
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Condition.Condition.Types.ConditionItem> ConditionConfig {
      get { return conditionConfig_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Condition);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Condition other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!conditionConfig_.Equals(other.conditionConfig_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= conditionConfig_.GetHashCode();
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
      conditionConfig_.WriteTo(output, _repeated_conditionConfig_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += conditionConfig_.CalculateSize(_repeated_conditionConfig_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Condition other) {
      if (other == null) {
        return;
      }
      conditionConfig_.Add(other.conditionConfig_);
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
            conditionConfig_.AddEntriesFrom(input, _repeated_conditionConfig_codec);
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Condition message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class ConditionItem : pb::IMessage<ConditionItem> {
        private static readonly pb::MessageParser<ConditionItem> _parser = new pb::MessageParser<ConditionItem>(() => new ConditionItem());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<ConditionItem> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Condition.Condition.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ConditionItem() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ConditionItem(ConditionItem other) : this() {
          conditionId_ = other.conditionId_;
          baseConditionId_ = other.baseConditionId_;
          paras_ = other.paras_.Clone();
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public ConditionItem Clone() {
          return new ConditionItem(this);
        }

        /// <summary>Field number for the "conditionId" field.</summary>
        public const int ConditionIdFieldNumber = 1;
        private long conditionId_;
        /// <summary>
        /// @name 条件编号
        /// @key
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long ConditionId {
          get { return conditionId_; }
          set {
            conditionId_ = value;
          }
        }

        /// <summary>Field number for the "baseConditionId" field.</summary>
        public const int BaseConditionIdFieldNumber = 2;
        private long baseConditionId_;
        /// <summary>
        /// @name 基础条件编号
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public long BaseConditionId {
          get { return baseConditionId_; }
          set {
            baseConditionId_ = value;
          }
        }

        /// <summary>Field number for the "paras" field.</summary>
        public const int ParasFieldNumber = 3;
        private static readonly pb::FieldCodec<int> _repeated_paras_codec
            = pb::FieldCodec.ForInt32(26);
        private readonly pbc::RepeatedField<int> paras_ = new pbc::RepeatedField<int>();
        /// <summary>
        /// @name 参数
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pbc::RepeatedField<int> Paras {
          get { return paras_; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as ConditionItem);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(ConditionItem other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (ConditionId != other.ConditionId) return false;
          if (BaseConditionId != other.BaseConditionId) return false;
          if(!paras_.Equals(other.paras_)) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (ConditionId != 0L) hash ^= ConditionId.GetHashCode();
          if (BaseConditionId != 0L) hash ^= BaseConditionId.GetHashCode();
          hash ^= paras_.GetHashCode();
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
          if (ConditionId != 0L) {
            output.WriteRawTag(8);
            output.WriteInt64(ConditionId);
          }
          if (BaseConditionId != 0L) {
            output.WriteRawTag(16);
            output.WriteInt64(BaseConditionId);
          }
          paras_.WriteTo(output, _repeated_paras_codec);
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (ConditionId != 0L) {
            size += 1 + pb::CodedOutputStream.ComputeInt64Size(ConditionId);
          }
          if (BaseConditionId != 0L) {
            size += 1 + pb::CodedOutputStream.ComputeInt64Size(BaseConditionId);
          }
          size += paras_.CalculateSize(_repeated_paras_codec);
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(ConditionItem other) {
          if (other == null) {
            return;
          }
          if (other.ConditionId != 0L) {
            ConditionId = other.ConditionId;
          }
          if (other.BaseConditionId != 0L) {
            BaseConditionId = other.BaseConditionId;
          }
          paras_.Add(other.paras_);
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
              case 8: {
                ConditionId = input.ReadInt64();
                break;
              }
              case 16: {
                BaseConditionId = input.ReadInt64();
                break;
              }
              case 26:
              case 24: {
                paras_.AddEntriesFrom(input, _repeated_paras_codec);
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
