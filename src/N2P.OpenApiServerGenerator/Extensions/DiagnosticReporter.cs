using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.CodeAnalysis;
using N2P.OpenApiServerGenerator.Model;

namespace N2P.OpenApiServerGenerator.Extensions
{
    internal class DiagnosticReporter
    {
        private readonly GeneratorExecutionContext _context;
        private const string IdPrefix = "N2POASG-";


        internal DiagnosticReporter(GeneratorExecutionContext context)
        {
            _context = context;
        }
        internal void ReportMissingAssemblyReference(string assemblyName, int assemblyMajorVersion)
        {
            var descriptor = new DiagnosticDescriptor($"{IdPrefix}001", "Missing Required Assembly References",
                "[{0}] package with major version: [{1}] is a must. The generated code implements the interfaces from it.", "DependencyCheck",
                DiagnosticSeverity.Error, true);
            _context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None,
                assemblyName, assemblyMajorVersion));
        }
        internal void ReportUncaughtException(Exception e)
        {
            var descriptor = new DiagnosticDescriptor($"{IdPrefix}002", "Uncaught Exception",
                "An uncaught exception occured in Source Generator. - {0}", "Unknown",
                DiagnosticSeverity.Error, true);
            _context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None,
                e));
        }

        internal void ReportMissingConfigurationFile()
        {
            var descriptor = new DiagnosticDescriptor($"{IdPrefix}003",
                "Source Generator Configuration not found",
                "Could not find configuration file. A yaml file should must be supplied via 'AdditionalFiles' element with '{0}' attribute set to `true`. Also make sure '{0}' is made visible to compiler by using <CompilerVisibleItemMetaData Include=\"AdditionalFiles\" MetaDataName=\"{0}\"/>",
                "Configuration", DiagnosticSeverity.Error, true);
            _context.ReportDiagnostic(Diagnostic.Create(descriptor,
                Location.None, SourceGeneratorOptions.ConfigurationFileDiscriminator));
        }

        internal void ReportConfigurationFileLocation(string filePath)
        {
            var descriptor = new DiagnosticDescriptor($"{IdPrefix}004", "Configuration Found",
                "Trying to read Configuration File. Path: {0}", "Configuration",
                DiagnosticSeverity.Info, true);
            _context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None,
                filePath));
        }

        internal void ReportConfigurationReadProblem(string filePath, Exception e)
        {
            var descriptor = new DiagnosticDescriptor($"{IdPrefix}005", "Configuration is null",
                "Unable to read configuration. File used: [{0}], Error: '{1}'", "Configuration", DiagnosticSeverity.Error,
                true);
            _context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None,
                filePath, e));
        }

        internal void ReportConfigurationFieldsValidation(List<ValidationResult> failedResults)
        {
            var descriptor = new DiagnosticDescriptor($"{IdPrefix}006", "Missing Required Parameters",
                "Configuration fields validation failed. [{0}]", "Configuration",
                DiagnosticSeverity.Error, true);

            _context.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None,
                string.Join(',',
                    failedResults.Aggregate(string.Empty,
                        (current, result) => current + $"{result.ErrorMessage} | "))));
        }
    }
}
