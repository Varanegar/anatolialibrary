using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;
using WcfAppServer.Common.Serializers;

namespace WcfAppServer.Configuration
{
    public static class WcfBindingHelper
    {
        public static Binding GetInferredBinding(string address, bool isMexBinding)
        {
            if (isMexBinding)
            {
                if (address.StartsWith("net.pipe"))
                    return CreateMexNamedPipeBinding();

                if (address.StartsWith("net.tcp"))
                    return CreateMexNetTcpBinding();

                if (address.StartsWith("http"))
                    return CreateMexHttpBinding();
            }
            else
            {
                if (address.StartsWith("net.pipe"))
                    return CreateNamedPipeBinding();

                if (address.StartsWith("net.tcp"))
                    return CreateTcpBinding();

                if (address.StartsWith("http://"))
                    return CreateWebHttpBinding();

                if (address.StartsWith("https://"))
                    return CreateWsHttpBinding();
            }
            throw new Exception("Cannot infer binding from address, please use net.tcp or http:// address");
        }

        private static Binding CreateTcpBinding()
        {
            const int setting = 25*1024*1024;
            var thirtyMinutes = new TimeSpan(0, 30, 0);

            var binding = new NetTcpBinding
                              {
                                  CloseTimeout = thirtyMinutes,
                                  //HostNameComparisonMode = HostNameComparisonMode.WeakWildcard,
                                  //ListenBacklog = setting,
                                  MaxBufferPoolSize = setting,
                                  MaxBufferSize = setting,
                                  MaxConnections = setting,
                                  MaxReceivedMessageSize = setting,
                                  Name = "WcfAppServer.TcpBinding",
                                  Namespace = "http://WcfAppServer.org",
                                  OpenTimeout = thirtyMinutes,
                                  PortSharingEnabled = true,
                                  ReceiveTimeout = thirtyMinutes,
                                  SendTimeout = thirtyMinutes 
                                  //TransactionFlow = false,
                                  //TransactionProtocol = TransactionProtocol.Default,
                                  //TransferMode = TransferMode.Buffered
                              };

            //security and reliable omitted

            return binding;
        }

        private static Binding CreateWebHttpBinding()
        {
            Binding binding = new WebHttpBinding();
            return binding;
        }

        private static Binding CreateWsHttpBinding()
        {
            Binding binding = new WSHttpBinding();
            return binding;
        }

        private static Binding CreateNamedPipeBinding()
        {
            Binding binding = new NetNamedPipeBinding();
            return binding;
        }

        private static Binding CreateMexHttpBinding()
        {
            return MetadataExchangeBindings.CreateMexHttpBinding();
        }

        private static Binding CreateMexNetTcpBinding()
        {
            return MetadataExchangeBindings.CreateMexTcpBinding();
        }

        private static Binding CreateMexNamedPipeBinding()
        {
            return MetadataExchangeBindings.CreateMexNamedPipeBinding();
        }

        public static Binding Deserialize(Stream stream)
        {
            //string tempString = System.Text.UTF8Encoding.UTF8.GetString(((System.IO.MemoryStream)stream).ToArray());
            DataContractSerializer serializer = GetBindingSerializer();
            var reader = new XmlTextReader(stream);
            var binding = (Binding) serializer.ReadObject(reader);

            return binding;
        }


        public static Stream SerializeToStream(Binding binding)
        {
            DataContractSerializer serializer = GetBindingSerializer();

            var memoryStream = new MemoryStream();
            var writer = new XmlTextWriter(memoryStream, Encoding.ASCII);
            serializer.WriteObject(writer, binding);
            writer.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }

        private static DataContractSerializer GetBindingSerializer()
        {
            IList<Type> types = new List<Type>(4)
                                    {
                                        typeof (UTF8Encoding),
                                        typeof (EncoderExceptionFallback),
                                        typeof (DecoderExceptionFallback),
                                        typeof (WebHttpBinding),
                                        typeof (NetNamedPipeBinding),
                                        typeof (BasicHttpBinding),
                                        typeof (WSHttpBinding),
                                        typeof (NetTcpBinding),
                                        //typeof (NetTcpContextBinding),
                                        typeof (SurrogateOleTransactions)
                                        //typeof (Basic256SecurityAlgorithmSuite)
                                    };

            var dcs = new DataContractSerializer(typeof (Binding),
                                                 types.AsEnumerable(),
                                                 int.MaxValue,
                                                 false,
                                                 true,
                                                 new BindingDataContractSurrogate());
            return dcs;
        }
    }
}