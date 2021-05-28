using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using N2P.OpenApiServerGenerator.Extensions;
using N2P.OpenApiServerGenerator.Model;

namespace N2P.OpenApiServerGenerator
{
    [Generator]
    public class OpenApiServerGenerator : ISourceGenerator
    {
        private readonly ImmutableDictionary<string, int> _requiredDependenciesForGeneratedCode;
        private GeneratorExecutionContext _context;
        private DiagnosticReporter _diagnosticReporter;
        private SourceGeneratorOptions _options;

        public OpenApiServerGenerator()
        {
            _requiredDependenciesForGeneratedCode = ImmutableDictionary.Create<string, int>().Add("MediatR", 9);
        }
        public void Initialize(GeneratorInitializationContext context)
        {
            //If we need any work to be done, Post Generator Initialization, we should register the RegisterForPostInitialization callback.
            //If we need any changes to applied to the generated source based on the user code changes, we should register the RegisterForSyntaxNotifications callback.
        }

        public void Execute(GeneratorExecutionContext context)
        {
            _context = context;
            SetUp();
            _diagnosticReporter = new DiagnosticReporter(context);
            try
            {
                if (EnsureRequiredAssembliesReferenced() && ReadConfigurationOptions() &&
                    EnsureRequiredConfigurationParametersSupplied())
                {
                    // Proceed to Read specification and Generate Source.
                }
            }
            catch (Exception e)
            {
                _diagnosticReporter.ReportUncaughtException(e);
            }

        }

        private bool ReadConfigurationOptions()
        {
            var isReadSuccess = false;
            if (!IsConfigurationFilePresent(out var filePath))
            {
                isReadSuccess = false;
                _diagnosticReporter.ReportMissingConfigurationFile();
            }
            else
            {
                IConfigurationRoot configuration = null;
                try
                {
                    var configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.AddYamlFile(filePath, false);
                    configuration = configurationBuilder.Build();
                }
                catch (Exception e)
                {
                    _diagnosticReporter.ReportConfigurationReadProblem(filePath, e);
                    return false;
                }
                _options = new SourceGeneratorOptions();
                configuration.Bind(_options, o => o.BindNonPublicProperties = true);

                isReadSuccess = true;
            }

            return isReadSuccess;
        }

        private bool IsConfigurationFilePresent(out string filePath)
        {
            var found = false;
            filePath = string.Empty;
            foreach (var file in _context.AdditionalFiles)
            {
                if (Path.GetExtension(file.Path).Equals(".yaml", StringComparison.OrdinalIgnoreCase) &&
                    _context.AnalyzerConfigOptions.GetOptions(file).TryGetValue(
                        $"build_metadata.additionalfiles.{SourceGeneratorOptions.ConfigurationFileDiscriminator}".ToLower(),
                        out var genConfValue) &&
                    bool.TryParse(genConfValue, out var isGeneratorConfigurationFile) && isGeneratorConfigurationFile)
                {
                    found = true;
                    filePath = file.Path;
                    _diagnosticReporter.ReportConfigurationFileLocation(file.Path);
                    break;
                }
            }

            return found;
        }

        private bool EnsureRequiredAssembliesReferenced()
        {
            foreach (var (assemblyName, assemblyMajorVersion) in _requiredDependenciesForGeneratedCode)
            {
                if (_context.Compilation.ReferencedAssemblyNames.Any(ai =>
                    ai.Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) &&
                    ai.Version.Major.Equals(assemblyMajorVersion)))
                {
                    continue;
                }

                _diagnosticReporter.ReportMissingAssemblyReference(assemblyName, assemblyMajorVersion);
                return false;
            }

            return true;
        }

        private bool EnsureRequiredConfigurationParametersSupplied()
        {
            var failedResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(_options, new ValidationContext(_options),
                failedResults, true))
            {
                _diagnosticReporter.ReportConfigurationFieldsValidation(failedResults);
            }
            return failedResults.Count == 0;
        }

        /// <summary>
        /// Couldn't find a Good name to this. Any suggestions are welcome. The responsibility of this method is to setup debugging and other necessary things in place, before proceeding to actual work.
        /// </summary>
        private void SetUp()
        {
            if (_context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.EnableSourceGeneratorDebug".ToLower(),
                    out var enableDebugValue) && bool.TryParse(enableDebugValue, out var enableDebugSwitch) &&
                enableDebugSwitch && !Debugger.IsAttached)
            {
                Debugger.Launch();
            }
        }
    }
}
