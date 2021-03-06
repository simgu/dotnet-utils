﻿using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace EMG.Utilities.ServiceModel
{
    public static class BindingExtensions
    {
        public static BasicHttpBinding UseHttps(this BasicHttpBinding binding, Action<HttpTransportSecurity> configure = null)
        {
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            configure?.Invoke(binding.Security.Transport);
            return binding;
        }

        public static BasicHttpBinding WithNoSecurity(this BasicHttpBinding binding)
        {
            binding.Security.Mode = BasicHttpSecurityMode.None;
            return binding;
        }

        public static WSHttpBinding UseHttps(this WSHttpBinding binding, Action<HttpTransportSecurity> configure = null)
        {
            binding.Security.Mode = SecurityMode.Transport;
            configure?.Invoke(binding.Security.Transport);
            return binding;
        }

        public static WSHttpBinding WithNoSecurity(this WSHttpBinding binding)
        {
            binding.Security.Mode = SecurityMode.None;
            return binding;
        }

        public static NetTcpBinding UseSecureChannel(this NetTcpBinding binding, Action<TcpTransportSecurity> configure = null)
        {
            binding.Security.Mode = SecurityMode.Transport;
            configure?.Invoke(binding.Security.Transport);
            return binding;
        }

        public static NetTcpBinding WithNoSecurity(this NetTcpBinding binding)
        {
            binding.Security.Mode = SecurityMode.None;
            return binding;
        }

        public static Binding WithNoSecurity(this Binding binding)
        {
            return binding switch
            {
                BasicHttpBinding basic => basic.WithNoSecurity() as Binding,
                NetTcpBinding netTcp => netTcp.WithNoSecurity() as Binding,
                WSHttpBinding wsHttp => wsHttp.WithNoSecurity() as Binding,
#if !NETSTANDARD
                NetNamedPipeBinding netNamed => netNamed.WithNoSecurity() as Binding,
#endif
                _ => throw new NotSupportedException()
            };
        }

#if !NETSTANDARD

        public static NetNamedPipeBinding UseSecureChannel(this NetNamedPipeBinding binding, Action<NamedPipeTransportSecurity> configure = null)
        {
            binding.Security.Mode = NetNamedPipeSecurityMode.Transport;
            configure?.Invoke(binding.Security.Transport);
            return binding;
        }

        public static NetNamedPipeBinding WithNoSecurity(this NetNamedPipeBinding binding)
        {
            binding.Security.Mode = NetNamedPipeSecurityMode.None;
            return binding;
        }
#endif
    }
}