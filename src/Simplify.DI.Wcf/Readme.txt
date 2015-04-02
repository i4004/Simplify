To use Simplify.DI.Wcf please add Factory="Simplify.DI.Wcf.SimplifyServiceHostFactory" to your WCF service *.svc file.

svc file example:
<%@ ServiceHost Language="C#" Debug="true" Service="MyNamespace.Service" CodeBehind="Service.svc.cs" Factory="Simplify.DI.Wcf.SimplifyServiceHostFactory" %>


Simplify.DI.Wcf documentation: https://github.com/i4004/Simplify/wiki/Simplify.DI.Wcf