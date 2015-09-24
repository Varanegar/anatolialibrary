using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfAppServer.Interfaces.Serializers
{
    [DataContract]
    public class SurrogateOleTransactions
    {
    }

    public class BindingDataContractSurrogate : IDataContractSurrogate
    {
        private readonly Type _oleTransactionsProtocolType;

        public BindingDataContractSurrogate()
        {
            TryFindType("System.ServiceModel.OleTransactionsProtocol", out _oleTransactionsProtocolType);
        }

        #region IDataContractSurrogate Members

        public Type GetDataContractType(Type type)
        {
            return type.Equals(_oleTransactionsProtocolType)
                       ? typeof (SurrogateOleTransactions)
                       : type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            return obj.GetType().Equals(_oleTransactionsProtocolType)
                       ? new SurrogateOleTransactions()
                       : obj;
        }

        public object GetDeserializedObject(Object obj, Type targetType)
        {
            return targetType.Name == "TransactionProtocol"
                       ? TransactionProtocol.OleTransactions
                       : obj;
        }

        public Type GetReferencedTypeOnImport(string typeName,
                                              string typeNamespace,
                                              object customData)
        {
            return null;
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration,
                                                       CodeCompileUnit compileUnit)
        {
            return null;
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            return null;
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            return null;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
        }

        #endregion

        public static bool TryFindType(string typeName, out Type type)
        {
            type = null;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                    break;
            }
            return type != null;
        }
    }
}