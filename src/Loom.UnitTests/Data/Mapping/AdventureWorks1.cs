/*
System.Configuration.ConfigurationErrorsException: An error occurred creating the configuration section handler for activeMapCodeGenConfiguration: Could not load type 'Colossus.Framework.Data.Mapping.Configuration.ActiveMapCodeGenConfigurationSection' from assembly 'Colossus.Framework.Data'. (G:\Projects\Current\Colossus.Framework\src\Framework.UnitTests\Data\Mapping\AdventureWorks.map line 4) ---> System.TypeLoadException: Could not load type 'Colossus.Framework.Data.Mapping.Configuration.ActiveMapCodeGenConfigurationSection' from assembly 'Colossus.Framework.Data'.
   at System.Configuration.TypeUtil.GetTypeWithReflectionPermission(IInternalConfigHost host, String typeString, Boolean throwOnError)
   at System.Configuration.MgmtConfigurationRecord.CreateSectionFactory(FactoryRecord factoryRecord)
   at System.Configuration.BaseConfigurationRecord.FindAndEnsureFactoryRecord(String configKey, Boolean& isRootDeclaredHere)
   --- End of inner exception stack trace ---
   at System.Configuration.BaseConfigurationRecord.FindAndEnsureFactoryRecord(String configKey, Boolean& isRootDeclaredHere)
   at System.Configuration.BaseConfigurationRecord.GetSectionRecursive(String configKey, Boolean getLkg, Boolean checkPermission, Boolean getRuntimeObject, Boolean requestIsHere, Object& result, Object& resultRuntimeObject)
   at Colossus.Framework.Data.Mapping.CodeGeneration.CodeGenSession..ctor(String configurationFilePath)
   at Colossus.Framework.Data.Mapping.CodeGeneration.Processor..ctor(String configurationFilePath)
   at Colossus.ActiveData.CustomToolGenerator.GenerateCode(String inputFileName, String inputFileContent)
*/
