using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZigBeeNet.CodeGenerator.Zcl;
using Attribute = ZigBeeNet.CodeGenerator.Zcl.Attribute;

namespace ZigBeeNet.CodeGenerator
{
    public static class ZclProtocolCodeGenerator
    {
        // The following are offsets to the root package
        private static string _packageZcl = ".ZCL";
        private static string _packageZclField = _packageZcl + ".Field";
        private static string _packageZclCluster = _packageZcl + ".Clusters";
        private static string _packageZclProtocol = _packageZcl + ".Protocol";
        private static string _packageZclProtocolCommand = _packageZclCluster;
        private static string _packageTransactionPrefix = ".Transaction";
        //static string _packageZdp = ".zdo";
        //static string _packageZdpField = _packageZdp + ".field";
        //static string _packageZdpCommand = _packageZdp + ".command";
        //static string _packageZdpTransaction = _packageZdp + ".transaction";
        //static string _packageZdpDescriptors = _packageZdpField;

        private static int _lineLen = 120;
        private static string _generatedDate;
        private static string _outRootPath;

        private static bool FileCompare(string file1, string file2)
        {
            FileInfo f = new FileInfo(file1);
            if (!f.Exists)
            {
                return false;
            }
            f = new FileInfo(file2);
            if (!f.Exists)
            {
                return false;
            }

            StreamReader reader1 = new StreamReader(file1);
            StreamReader reader2 = new StreamReader(file2);

            string line1 = reader1.ReadLine();
            string line2 = reader2.ReadLine();

            bool areEqual = true;

            int lineNum = 1;

            while (line1 != null || line2 != null)
            {
                if (line1 == null || line2 == null)
                {
                    areEqual = false;

                    break;
                }
                else if (!line1.StartsWith("@Generated") && !line1.Equals(line2, StringComparison.CurrentCultureIgnoreCase))
                {
                    areEqual = false;

                    break;
                }

                line1 = reader1.ReadLine();
                line2 = reader2.ReadLine();

                lineNum++;
            }

            if (areEqual)
            {
                Console.WriteLine("Two files have same content.");
            }
            else
            {
                Console.WriteLine("Two files have different content. They differ at line " + lineNum);
                Console.WriteLine("File1 has " + line1 + " and File2 has " + line2 + " at line " + lineNum);
            }

            reader1.Close();
            reader2.Close();

            return areEqual;
        }

        private static void CopyFile(string source, string destination)
        {
            var src = new FileInfo(source);
            var dest = new FileInfo(destination);

            Directory.CreateDirectory(Path.GetDirectoryName(destination));

            File.Copy(source, destination, true);
        }

        private static void CompareFiles(string inFolder, string outFolder, string folder)
        {
            FileInfo[] files = Directory.GetFiles(inFolder + folder).Select(f => new FileInfo(f)).ToArray();

            DirectoryInfo[] dirs = Directory.GetDirectories(inFolder + folder).Select(d => new DirectoryInfo(d)).ToArray();

            foreach (var file in files)
            {
                Console.WriteLine("File: " + folder + "/" + file.Name);

                try
                {
                    if (!FileCompare(inFolder + folder + "/" + file.Name, outFolder + folder + "/" + file.Name))
                    {
                        CopyFile(inFolder + folder + "/" + file.Name, outFolder + folder + "/" + file.Name);
                        Console.WriteLine("File: " + folder + "/" + file.Name + " updated");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            foreach (var dir in dirs)
            {
                CompareFiles(inFolder, outFolder, folder + "/" + dir.Name);
            }
        }


        /**
         * The main method for running the code generator.
         *
         * @param args
         *            the command line arguments
         */
        public static void Generate(string[] args = null)
        {
            _generatedDate = DateTime.UtcNow.ToShortDateString() + " - " + DateTime.UtcNow.ToShortTimeString();

            string definitionFilePathZcl = "./Resources/zcl_definition.md";
            //string definitionFilePathZll = "./src/main/resources/zll_definition.md";
            //string definitionFilePathZdp = "./src/main/resources/zdp_definition.md";
            //string definitionFilePathOta = "./src/main/resources/ota_definition.md";
            //string definitionFilePathZse = "./src/main/resources/zse_definition.md";
            //string definitionFilePathMan = "./src/main/resources/manufacturer_definition.md";

            if(args != null && args.Length > 0)
                _outRootPath = args[0]; // "../../../../ZigBeeNet/ZCL/Clusters";

            Context contextZcl = new Context();
            FileInfo definitionFileZcl = new FileInfo(definitionFilePathZcl);

            //File definitionFileZse = new File(definitionFilePathZse);
            //File definitionFileOta = new File(definitionFilePathOta);
            //File definitionFileZll = new File(definitionFilePathZll);
            //File definitionFileMan = new File(definitionFilePathMan);

            if (!(definitionFileZcl.Exists /*&& definitionFileOta.exists() && definitionFileZll.exists()*/))
            {
                Console.WriteLine("Definition file does not exist: " + definitionFilePathZcl);
            }
            else
            {
                try
                {
                    contextZcl.Lines = new List<string>(File.ReadAllLines(definitionFilePathZcl, Encoding.UTF8));
                    //contextZcl.Lines.addAll(new ArrayList<string>(FileUtils.readLines(definitionFileOta, "UTF-8")));
                    //contextZcl.Lines.addAll(new ArrayList<string>(FileUtils.readLines(definitionFileZll, "UTF-8")));
                    //contextZcl.Lines.addAll(new ArrayList<string>(FileUtils.readLines(definitionFileZse, "UTF-8")));
                    //contextZcl.Lines.addAll(new ArrayList<string>(FileUtils.readLines(definitionFileMan, "UTF-8")));
                    GenerateZclCode(contextZcl/*, sourceRootFile, packageRoot*/);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Reading lines from Zcl definition file failed: " + definitionFileZcl.FullName + "\n" + e.ToString());
                }
            }
        }

        //Context contextZdp = new Context();
        //FileInfo definitionFileZdp = new FileInfo(definitionFilePathZdp);
        //if (!definitionFileZdp.Exists)
        //{
        //    Console.WriteLine("Definition file does not exist: " + definitionFilePathZdp);
        //}
        //else
        //{
        //    try
        //    {
        //        contextZdp.lines = new ArrayList<string>(FileUtils.readLines(definitionFileZdp, "UTF-8"));
        //        generateZdpCode(contextZdp, sourceRootFile, packageRoot);
        //    }
        //    catch (final IOException e) {
        //        Console.WriteLine(
        //                "Reading lines from Zdp definition file failed: " + definitionFileZdp.getAbsolutePath());
        //        e.printStackTrace();
        //        return;
        //    }
        //    }

        //    final string packagePath = getPackagePath(sourceRootFile, packageRoot);
        //    final File packageFile = getPackageFile(packagePath);

        //    try
        //    {
        //        final LinkedList<DataType> dataTypes = new LinkedList<DataType>(contextZcl.dataTypes.values());

        //        // Add any types that are not defined in the autogenerated code
        //        final DataType dataType = new DataType();
        //        dataType.dataTypeName = "EXTENDED_PANID";
        //        dataType.dataTypeType = "EXTENDED_PANID";

        //        dataType.dataTypeClass = ZclDataType.getDataTypeMapping().get("EXTENDED_PANID").dataClass;
        //        dataTypes.add(dataType);

        //        boolean addIt;
        //        for (DataType newType : contextZdp.dataTypes.values())
        //        {
        //            addIt = true;
        //            for (DataType checkType : dataTypes)
        //            {
        //                if (checkType.dataTypeType.equals(newType.dataTypeType))
        //                {
        //                    addIt = false;
        //                }
        //            }
        //            if (addIt)
        //            {
        //                dataTypes.add(newType);
        //            }
        //        }

        //        generateZclDataTypeEnumeration(dataTypes, packageRoot, packageFile);

        //        string inRootPath = sourceRootPath.substring(0, sourceRootPath.length() - 1);
        //        compareFiles(inRootPath, outRootPath, "");
        //    }
        //    catch (final IOException e) {
        //        Console.WriteLine("Failed to generate data types enumeration.");
        //        e.printStackTrace();
        //        return;
        //    }
        //    }

        public static void GenerateZclCode(Context context/*, FileInfo sourceRootPath, string packageRoot*/)
        {
            ZclProtocolDefinitionParser.ParseProfiles(context);

            //string packagePath = GetPackagePath(sourceRootPath, packageRoot);
            //FileInfo packageFile = GetPackageFile(packagePath);

            //try
            //{
            //    List<DataType> dataTypes = new List<DataType>(context.DataTypes.Values);
            //    dataTypes.AddRange(context.DataTypes.Values);

            //    GenerateZclDataTypeEnumeration(dataTypes, packageRoot, packageFile);
            //}
            //catch (IOException e)
            //{
            //    Console.WriteLine("Failed to generate data types enumeration.\n" + e.ToString());
            //    return;
            //}

            //try
            //{
            //    generateZclProfileTypeEnumeration(context, packageRoot, packageFile);
            //}
            //catch (final IOException e) {
            //Console.WriteLine("Failed to generate profile enumeration.");
            //e.printStackTrace();
            //return;
            //}

            //try
            //{
            //    generateZclClusterTypeEnumeration(context, packageRoot, packageFile);
            //}
            //catch (final IOException e) {
            //    Console.WriteLine("Failed to generate cluster enumeration.");
            //    e.printStackTrace();
            //    return;
            //}

            //try
            //{
            //    // generateZclCommandTypeEnumerationXXXXX(context, packageRoot, packageFile);
            //    generateZclCommandTypeEnumeration(context, packageRoot, packageFile);
            //}
            //catch (final IOException e) {
            //    Console.WriteLine("Failed to generate command enumeration.");
            //    e.printStackTrace();
            //    return;
            //}

            //try
            //{
            //    generateAttributeEnumeration(context, packageRoot, sourceRootPath);
            //}
            //catch (final IOException e) {
            //    Console.WriteLine("Failed to generate attribute enum classes.");
            //    e.printStackTrace();
            //    return;
            //}

            //try
            //{
            //    generateFieldEnumeration(context, packageRoot, sourceRootPath);
            //}
            //catch (final IOException e) {
            //    Console.WriteLine("Failed to generate field enum classes.");
            //    e.printStackTrace();
            //    return;
            //}

            try
            {
                GenerateZclCommandClasses(context/*, packageRoot, sourceRootPath*/);
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to generate profile message classes.\n" + e.ToString());
                return;
            }

            try
            {
                GenerateZclClusterClasses(context/*, packageRoot, sourceRootPath*/);
            }
            catch (IOException e)
            {
                Console.WriteLine("Failed to generate cluster classes.\n" + e.ToString());
                return;
            }
        }

        //public static void generateZdpCode(final Context context, final File sourceRootPath, final string packageRoot)
        //{
        //    ZclProtocolDefinitionParser.parseProfiles(context);

        //    try
        //    {
        //        generateZdpCommandClasses(context, packageRoot, sourceRootPath);
        //    }
        //    catch (final IOException e) {
        //        Console.WriteLine("Failed to generate profile message classes.");
        //        e.printStackTrace();
        //        return;
        //    }

        //    try
        //    {
        //        generateZdoCommandTypeEnumeration(context, packageRoot, sourceRootPath);
        //    }
        //    catch (final IOException e) {
        //        Console.WriteLine("Failed to generate command enumeration.");
        //        e.printStackTrace();
        //        return;
        //    }

        //    }

        private static void OutputClassDoc(StringBuilder code, string description)
        {
            code.AppendLine("/**");
            code.AppendLine(" * " + description);
            code.AppendLine(" *");
            code.AppendLine(" * Code is auto-generated. Modifications may be overwritten!");
            code.AppendLine(" *");
            code.AppendLine(" */");
        }

        private static DirectoryInfo GetPackageFile(string packagePath)
        {
            var dirInfo = Directory.CreateDirectory(packagePath);
            return dirInfo;
        }

        //private static string GetPackagePath(File sourceRootPath, string packageRoot)
        //{
        //    return sourceRootPath.getAbsolutePath() + File.separator + packageRoot.replace(".", File.separator);
        //}

        //        private static void generateZclDataTypeEnumeration(LinkedList<DataType> dataTypes, final string packageRootPrefix,
        //                File sourceRootPath) throws IOException
        //        {
        //            final string className = "ZclDataType";

        //        final string packageRoot = packageRootPrefix + packageZclProtocol;
        //        final string packagePath = getPackagePath(sourceRootPath, packageZclProtocol);
        //        final File packageFile = getPackageFile(packagePath);

        //        final PrintWriter out = getClassOut(packageFile, className);
        //        CodeGeneratorUtil.outputLicense(out);

        //                Console.WriteLine("package " + packageRoot + ";");

        //                Console.WriteLine();
        //                Console.WriteLine("import java.util.Calendar;");
        //                Console.WriteLine("import java.util.HashMap;");
        //                Console.WriteLine("import java.util.Map;");
        //                Console.WriteLine();
        //                Console.WriteLine("import javax.annotation.Generated;");
        //                Console.WriteLine("import " + packageRootPrefix + packageZclField + ".*;");
        //                Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclStatus;");
        //                Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZdoStatus;");
        //                Console.WriteLine("import " + packageRootPrefix + packageZdpDescriptors + ".*;");
        //                Console.WriteLine("import " + packageRootPrefix + "." + "IeeeAddress" + ";");
        //                Console.WriteLine("import " + packageRootPrefix + "." + "ExtendedPanId" + ";");
        //                Console.WriteLine();
        //                outputClassJavaDoc(out, "Enumeration of the ZCL data types");
        //        outputClassGenerated(out);
        //        Console.WriteLine("public enum " + className + " {");

        //                DataType newDataType = new DataType();

        //        newDataType = new DataType();
        //        newDataType.dataTypeName = "Unsigned 8 bit Integer Array";
        //                newDataType.dataTypeType = "UNSIGNED_8_BIT_INTEGER_ARRAY";
        //                newDataType.dataTypeClass = ZclDataType.getDataTypeMapping().get("UNSIGNED_8_BIT_INTEGER_ARRAY").dataClass;
        //                dataTypes.add(newDataType);

        //                newDataType = new DataType();
        //        newDataType.dataTypeName = "ZigBee Data Type";
        //                newDataType.dataTypeType = "ZIGBEE_DATA_TYPE";
        //                newDataType.dataTypeClass = ZclDataType.getDataTypeMapping().get("ZIGBEE_DATA_TYPE").dataClass;
        //                dataTypes.add(newDataType);

        //                // final LinkedList<DataType> dataTypes = new LinkedList<DataType>(context.dataTypes.values());
        //                for (final DataType dataType : dataTypes)
        //                {
        //                    DataTypeMap zclDataType = ZclDataType.getDataTypeMapping().get(dataType.dataTypeType);
        //        final string dataTypeClass;
        //                    if (dataType.dataTypeClass.contains("<"))
        //                    {
        //                        dataTypeClass = dataType.dataTypeClass.substring(dataType.dataTypeClass.indexOf("<") + 1,
        //                                dataType.dataTypeClass.indexOf(">"));
        //                    }
        //                    else
        //                    {
        //                        dataTypeClass = dataType.dataTypeClass;
        //                    }
        //            out.print("    " + dataType.dataTypeType + "(\"" + dataType.dataTypeName + "\", " + dataTypeClass + ".class"
        //                    + ", " + string.format("0x%02X", zclDataType.id) + ", " + zclDataType.analogue + ")");
        //                    Console.WriteLine(dataTypes.getLast().equals(dataType) ? ';' : ',');
        //                }

        //                Console.WriteLine();
        //                Console.WriteLine("    private final string label;");
        //                Console.WriteLine("    private final Class<?> dataClass;");
        //                Console.WriteLine("    private final int id;");
        //                Console.WriteLine("    private final boolean analogue;");
        //                Console.WriteLine("    private static Map<Integer, " + className + "> codeTypeMapping;");
        //                Console.WriteLine();

        //                Console.WriteLine("    static {");
        //                Console.WriteLine("        codeTypeMapping = new HashMap<Integer, " + className + ">();");
        //                Console.WriteLine("        for (" + className + " s : values()) {");
        //                Console.WriteLine("            codeTypeMapping.put(s.id, s);");
        //                Console.WriteLine("        }");
        //                Console.WriteLine("    }");
        //                Console.WriteLine();
        //                Console.WriteLine("    " + className
        //                        + "(final string label, final Class<?> dataClass, final int id, final boolean analogue) {");
        //                Console.WriteLine("        this.label = label;");
        //                Console.WriteLine("        this.dataClass = dataClass;");
        //                Console.WriteLine("        this.id = id;");
        //                Console.WriteLine("        this.analogue = analogue;");
        //                Console.WriteLine("    }");
        //                Console.WriteLine();

        //                Console.WriteLine("    public static " + className + " getType(int id) {");
        //                Console.WriteLine("        return codeTypeMapping.get(id);");
        //                Console.WriteLine("    }");

        //                Console.WriteLine();
        //                Console.WriteLine("    public string getLabel() {");
        //                Console.WriteLine("        return label;");
        //                Console.WriteLine("    }");
        //                Console.WriteLine();
        //                Console.WriteLine("    public Class<?> getDataClass() {");
        //                Console.WriteLine("        return dataClass;");
        //                Console.WriteLine("    }");
        //                Console.WriteLine();
        //                Console.WriteLine("    public int getId() {");
        //                Console.WriteLine("        return id;");
        //                Console.WriteLine("    }");
        //                Console.WriteLine();
        //                Console.WriteLine("    public boolean isAnalog() {");
        //                Console.WriteLine("        return analogue;");
        //                Console.WriteLine("    }");
        //                Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //            }

        private static void OutputClassGenerated(StringBuilder builder)
        {
            builder.AppendLine("/* Autogenerated: " + _generatedDate + " */");
        }

        //    private static void generateZclProfileTypeEnumeration(Context context, string packageRootPrefix,
        //            File sourceRootPath) throws IOException
        //{
        //    final string className = "ZigBeeProfileType";

        //    final string packageRoot = packageRootPrefix;
        //    final string packagePath = getPackagePath(sourceRootPath, "");
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    CodeGeneratorUtil.outputLicense(out);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();
        //    Console.WriteLine("import java.util.Map;");
        //    Console.WriteLine("import java.util.HashMap;");
        //    Console.WriteLine();
        //    Console.WriteLine("import javax.annotation.Generated;");

        //    Console.WriteLine();
        //    outputClassJavaDoc(out, "Enumeration of ZigBee profile types");
        //    outputClassGenerated(out);
        //    Console.WriteLine("public enum " + className + " {");

        //    Console.WriteLine("    UNKNOWN(-1, \"Unknown Profile\"),");
        //    final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            out.print("    " + profile.profileType + "(" + string.format("0x%04X", profile.profileId) + ", \""
        //                    + profile.profileName + "\")");
        //            Console.WriteLine(profiles.getLast().equals(profile)? ';' : ',');
        //        }

        //Console.WriteLine();
        //        Console.WriteLine("    /*");
        //        Console.WriteLine("     * The ZigBee profile ID");
        //        Console.WriteLine("     */");
        //        Console.WriteLine("    private final int profileId;");
        //        Console.WriteLine();
        //        Console.WriteLine("    /*");
        //        Console.WriteLine("     * The ZigBee profile label");
        //        Console.WriteLine("     */");
        //        Console.WriteLine("    private final string label;");
        //        Console.WriteLine();
        //        Console.WriteLine("    /**");
        //        Console.WriteLine("     * Map containing the link of profile type value to the enum");
        //        Console.WriteLine("     */");
        //        Console.WriteLine("    private static Map<Integer, ZigBeeProfileType> map = null;");
        //        Console.WriteLine();

        //        Console.WriteLine("    static {");
        //        Console.WriteLine("        map = new HashMap<Integer, ZigBeeProfileType>();");
        //        Console.WriteLine("        for (" + className + " profileType : values()) {");
        //        Console.WriteLine("            map.put(profileType.profileId, profileType);");
        //        Console.WriteLine("        }");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();

        //        Console.WriteLine("    " + className + "(final int profileId, final string label) {");
        //        Console.WriteLine("        this.profileId = profileId;");
        //        Console.WriteLine("        this.label = label;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    /*");
        //        Console.WriteLine("     * Get the ZigBee profile ID");
        //        Console.WriteLine("     *");
        //        Console.WriteLine("     * @ return the profile ID");
        //        Console.WriteLine("     */");
        //        Console.WriteLine("    public int getId() {");
        //        Console.WriteLine("        return profileId;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    /*");
        //        Console.WriteLine("     * Get the ZigBee profile label");
        //        Console.WriteLine("     *");
        //        Console.WriteLine("     * @ return the profile label");
        //        Console.WriteLine("     */");
        //        Console.WriteLine("    public string getLabel() {");
        //        Console.WriteLine("        return label;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();

        //        Console.WriteLine("    /**");
        //        Console.WriteLine("     * Get a {@link " + className + "} from an integer");
        //        Console.WriteLine("     *");
        //        Console.WriteLine("     * @param profileTypeValue integer value defining the profile type");
        //        Console.WriteLine("     * @return {@link " + className + "} or {@link #UNKNOWN} if the value could not be converted");
        //        Console.WriteLine("     */");
        //        Console.WriteLine("    public static " + className + " getProfileType(int profileTypeValue) {");
        //        Console.WriteLine("        if (map.get(profileTypeValue) == null) {");
        //        Console.WriteLine("            return UNKNOWN;");
        //        Console.WriteLine("        }");
        //        Console.WriteLine("        return map.get(profileTypeValue);");
        //        Console.WriteLine("    }");

        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        //    private static void generateZclClusterTypeEnumeration(Context context, string packageRootPrefix,
        //            File sourceRootPath) throws IOException
        //{
        //    final string className = "ZclClusterType";

        //    final string packageRoot = packageRootPrefix + packageZclProtocol;
        //    final string packagePath = getPackagePath(sourceRootPath, packageZclProtocol);
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    CodeGeneratorUtil.outputLicense(out);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();
        //    Console.WriteLine("import " + packageRootPrefix + ".ZigBeeProfileType;");
        //    Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclCluster;");
        //    Console.WriteLine("import " + packageRootPrefix + packageZclCluster + ".*;");
        //    Console.WriteLine();
        //    Console.WriteLine("import java.util.HashMap;");
        //    Console.WriteLine("import java.util.Map;");
        //    Console.WriteLine();
        //    Console.WriteLine("import javax.annotation.Generated;");

        //    Console.WriteLine();
        //    outputClassJavaDoc(out, "Enumeration of ZigBee Clusters");
        //    outputClassGenerated(out);
        //    Console.WriteLine("public enum " + className + " {");

        //    boolean first = true;
        //    final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                if (first == false) {
        //                    Console.WriteLine(",");
        //                }
        //                first = false;
        //                out.print("    " + cluster.clusterType + "(" + string.format("0x%04X", cluster.clusterId)
        //                        + ", ZigBeeProfileType." + profile.profileType + ", Zcl" + cluster.nameUpperCamelCase
        //                        + "Cluster.class, \"" + cluster.clusterName + "\")");
        //            }
        //        }
        //        Console.WriteLine(";");

        //        Console.WriteLine();
        //        Console.WriteLine(
        //                "    private static final Map<Integer, ZclClusterType> idValueMap = new HashMap<Integer, ZclClusterType>();");
        //        Console.WriteLine();
        //        Console.WriteLine("    private final int clusterId;");
        //        Console.WriteLine("    private final ZigBeeProfileType profileType;");
        //        Console.WriteLine("    private final string label;");
        //        Console.WriteLine("    private final Class<? extends ZclCluster> clusterClass;");
        //        Console.WriteLine();
        //        Console.WriteLine("    " + className
        //                + "(final int clusterId, final ZigBeeProfileType profileType, final Class<? extends ZclCluster>clusterClass, final string label) {");
        //        Console.WriteLine("        this.clusterId = clusterId;");
        //        Console.WriteLine("        this.profileType = profileType;");
        //        Console.WriteLine("        this.clusterClass = clusterClass;");
        //        Console.WriteLine("        this.label = label;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    static {");
        //        Console.WriteLine("        for (final ZclClusterType value : values()) {");
        //        Console.WriteLine("            idValueMap.put(value.clusterId, value);");
        //        Console.WriteLine("        }");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public int getId() {");
        //        Console.WriteLine("        return clusterId;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public ZigBeeProfileType getProfileType() {");
        //        Console.WriteLine("        return profileType;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public string getLabel() {");
        //        Console.WriteLine("        return label;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        // Console.WriteLine(" public string tostring() {");
        //        // Console.WriteLine(" return label;");
        //        // Console.WriteLine(" }");
        //        // Console.WriteLine();
        //        Console.WriteLine("    public Class<? extends ZclCluster> getClusterClass() {");
        //        Console.WriteLine("        return clusterClass;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public static ZclClusterType getValueById(final int clusterId) {");
        //        Console.WriteLine("        return idValueMap.get(clusterId);");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        //    private static void generateZclCommandTypeEnumerationXXXXX(Context context, string packageRootPrefix,
        //            File sourceRootPath) throws IOException
        //{

        //    final string className = "ZclCommandTypeXXX";

        //    final string packageRoot = packageRootPrefix + packageZclProtocol;
        //    final string packagePath = getPackagePath(sourceRootPath, packageZclProtocol);
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();
        //    outputClassJavaDoc(out, "Enumeration of ZCL commands");
        //    outputClassGenerated(out);
        //    Console.WriteLine("public enum " + className + " {");

        //    final LinkedList<string> valueRows = new LinkedList<string>();
        //        final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                {
        //                    final LinkedList<Command> commands = new LinkedList<Command>(cluster.received.values());
        //                    for (final Command command : commands) {
        //                        final boolean generic = cluster.clusterId == 65535;
        //                        valueRows.add("    " + command.commandType + "(" + command.commandId + ", ZclClusterType."
        //                                + cluster.clusterType + ", \"" + command.commandLabel + "\", true, " + generic + ")");
        //                    }
        //                }
        //                {
        //                    final LinkedList<Command> commands = new LinkedList<Command>(cluster.generated.values());
        //                    for (final Command command : commands) {
        //                        final boolean generic = cluster.clusterId == 65535;
        //                        valueRows.add("    " + command.commandType + "(" + command.commandId + ", ZclClusterType."
        //                                + cluster.clusterType + ", \"" + command.commandLabel + "\", false, " + generic + ")");
        //                    }
        //                }
        //            }
        //        }

        //        for (final string valueRow : valueRows) {
        //            out.print(valueRow);
        //Console.WriteLine(valueRows.getLast().equals(valueRow)? ';' : ',');
        //        }

        //        Console.WriteLine();
        //        Console.WriteLine("    private final int id;");
        //        Console.WriteLine("    private final ZclClusterType clusterType;");
        //        Console.WriteLine("    private final string label;");
        //        Console.WriteLine("    private final boolean received;");
        //        Console.WriteLine("    private final boolean generic;");
        //        Console.WriteLine();
        //        Console.WriteLine("    " + className
        //                + "(final int id, final ZclClusterType clusterType, final string label, final boolean received, final boolean generic) {");
        //        Console.WriteLine("        this.id = id;");
        //        Console.WriteLine("        this.clusterType = clusterType;");
        //        Console.WriteLine("        this.label = label;");
        //        Console.WriteLine("        this.received = received;");
        //        Console.WriteLine("        this.generic = generic;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public int getId() { return id; }");
        //        Console.WriteLine("    public ZclClusterType getClusterType() { return clusterType; }");
        //        Console.WriteLine("    public string getLabel() { return label; }");
        //        Console.WriteLine("    public boolean isReceived() { return received; }");
        //        Console.WriteLine("    public boolean isGeneric() { return generic; }");
        //        Console.WriteLine("    public string tostring() { return label; }");
        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        //    private static void generateZclAttributeTypeEnumeration(Context context, string packageRootPrefix,
        //            File sourceRootPath) throws IOException
        //{

        //    final string className = "ZclAttributeType";

        //    final string packageRoot = packageRootPrefix + packageZclProtocol;
        //    final string packagePath = getPackagePath(sourceRootPath, packageZclProtocol);
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    CodeGeneratorUtil.outputLicense(out);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();
        //    outputClassJavaDoc(out, "Enumeration of ZigBee attributes");
        //    outputClassGenerated(out);
        //    Console.WriteLine("public enum " + className + " {");

        //    boolean first = true;
        //    final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());

        //            for (final Cluster cluster : clusters) {
        //                for (final Attribute attribute : cluster.attributes.values()) {
        //                    if (first == false) {
        //                        Console.WriteLine(",");
        //                    }
        //                    first = false;
        //                    out.print("    " + attribute.enumName + "(0x" + string.format("%04X", cluster.clusterId) + ", 0x"
        //                            + string.format("%04X", attribute.attributeId) + ", ZclDataType." + attribute.dataType
        //                            + ")");
        //                }
        //            }
        //        }
        //        Console.WriteLine(";");

        //        Console.WriteLine();
        //        Console.WriteLine("    private final int clusterId;");
        //        Console.WriteLine("    private final int attributeId;");
        //        Console.WriteLine("    private final ZclAttributeType attributeType;");
        //        Console.WriteLine("    private final string label;");
        //        Console.WriteLine("    private final boolean received;");
        //        Console.WriteLine("    private final boolean generic;");
        //        Console.WriteLine();
        //        Console.WriteLine("    " + className + "(final int clusterId, final int attributeId, final ZclDataType dataType) {");
        //        Console.WriteLine("        this.id = id;");
        //        Console.WriteLine("        this.attributeType = attributeType;");
        //        Console.WriteLine("        this.label = label;");
        //        Console.WriteLine("        this.received = received;");
        //        Console.WriteLine("        this.generic = generic;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public int getId() { return id; }");
        //        Console.WriteLine("    public ZclAttributeType getAttributeType() { return attributeType; }");
        //        Console.WriteLine("    public string getLabel() { return label; }");
        //        Console.WriteLine("    public boolean isReceived() { return received; }");
        //        Console.WriteLine("    public boolean isGeneric() { return generic; }");
        //        Console.WriteLine("    public string tostring() { return label; }");
        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        //    private static void generateZclFieldTypeEnumeration(Context context, string packageRootPrefix, File sourceRootPath)
        //            throws IOException
        //{
        //    final string className = "ZclFieldType";

        //    final string packageRoot = packageRootPrefix + packageZclProtocol;
        //    final string packagePath = getPackagePath(sourceRootPath, packageZclProtocol);
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    CodeGeneratorUtil.outputLicense(out);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();
        //    outputClassJavaDoc(out, "Enumeration of ZCL fields");
        //    outputClassGenerated(out);
        //    Console.WriteLine("public enum " + className + " {");

        //    final LinkedList<string> valueRows = new LinkedList<string>();
        //        final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                final ArrayList<Command> commands = new ArrayList<Command>();
        //commands.addAll(cluster.received.values());
        //                commands.addAll(cluster.generated.values());
        //                for (final Command command : commands) {
        //                    final LinkedList<Field> fields = new LinkedList<Field>(command.fields.values());
        //                    for (final Field field : fields) {
        //                        valueRows.add("    " + field.fieldType + "(" + field.fieldId + ", ZclCommandType."
        //                                + command.commandType + ", \"" + field.fieldLabel + "\", ZclDataType." + field.dataType
        //                                + ")");
        //                    }
        //                }
        //            }
        //        }

        //        for (final string valueRow : valueRows) {
        //            out.print(valueRow);
        //Console.WriteLine(valueRows.getLast().equals(valueRow)? ';' : ',');
        //        }

        //        Console.WriteLine();
        //        Console.WriteLine("    private final int id;");
        //        Console.WriteLine("    private final ZclCommandType commandType;");
        //        Console.WriteLine("    private final string label;");
        //        Console.WriteLine("    private final ZclDataType dataType;");
        //        Console.WriteLine();
        //        Console.WriteLine("    " + className
        //                + "(final int id, final ZclCommandType commandType, final string label, final ZclDataType dataType) {");
        //        Console.WriteLine("        this.id = id;");
        //        Console.WriteLine("        this.commandType = commandType;");
        //        Console.WriteLine("        this.label = label;");
        //        Console.WriteLine("        this.dataType = dataType;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public int getId() { return id; }");
        //        Console.WriteLine("    public ZclCommandType getCommandType() { return commandType; }");
        //        Console.WriteLine("    public string getLabel() { return label; }");
        //        Console.WriteLine("    public ZclDataType getDataType() { return dataType; }");
        //        Console.WriteLine();
        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        private static void GenerateZclCommandClasses(Context context)
        {
            List<Profile> profiles = new List<Profile>(context.Profiles.Values);
            foreach (Profile profile in profiles)
            {

                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);
                foreach (Cluster cluster in clusters)
                {

                    List<Command> commands = new List<Command>();
                    commands.AddRange(cluster.Received.Values);
                    commands.AddRange(cluster.Generated.Values);

                    foreach (Command command in commands)
                    {
                        string packageRoot = "ZigBeeNet.ZCL." + cluster.ClusterType.Replace("_", "").ToUpper();
                        //string packagePath = GetPackagePath(sourceRootPath, packageRoot);
                        //final File packageFile = getPackageFile(packagePath);

                        string className = command.NameUpperCamelCase;
                        //final PrintWriter out = getClassOut(packageFile, className);

                        List<Field> fields = new List<Field>(command.Fields.Values);
                        bool fieldWithDataTypeList = false;
                        foreach (Field field in fields)
                        {
                            if (field.DataTypeClass.StartsWith("List"))
                            {
                                fieldWithDataTypeList = true;
                                break;
                            }
                        }
                        var code = new StringBuilder();

                        CodeGeneratorUtil.OutputLicense(code);

                        // code.AppendLine("import " + packageRootPrefix + packageZcl + ".ZclCommandMessage;");
                        //code.AppendLine("import " + packageRootPrefix + packageZcl + ".ZclCommand;");
                        // code.AppendLine("import " + packageRootPrefix + packageZcl + ".ZclField;");

                        code.AppendLine("using System;");
                        code.AppendLine("using System.Collections.Generic;");
                        code.AppendLine("using System.Linq;");
                        code.AppendLine("using System.Text;");
                        code.AppendLine("using ZigBeeNet.ZCL.Protocol;");
                        code.AppendLine("using ZigBeeNet.ZCL.Field;");
                        code.AppendLine("using ZigBeeNet.ZCL.Clusters." + cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + ";");

                        // code.AppendLine("import " + packageRootPrefix + packageZclProtocol + ".ZclClusterType;");
                        // code.AppendLine("import " + packageRootPrefix + packageZclProtocol + ".ZclCommandType;");
                        // if (!fields.isEmpty()) {
                        // code.AppendLine("import " + packageRootPrefix + packageZclProtocol + ".ZclFieldType;");
                        // if (fieldWithDataTypeList) {
                        // code.AppendLine("import " + packageRootPrefix + packageZclField + ".*;");
                        // }
                        // }
                        // code.AppendLine("import java.util.Map;");
                        // code.AppendLine("import java.util.HashMap;");

                        foreach (Field field in fields)
                        {
                            string packageName;
                            //if (field.DataTypeClass.Contains("Descriptor"))
                            //{
                            //    packageName = packageZdpDescriptors;
                            //}
                            //else
                            //{
                            packageName = _packageZclField;
                            //}

                            string typeName;
                            if (field.DataTypeClass.StartsWith("List"))
                            {
                                typeName = field.DataTypeClass;
                                typeName = typeName.Substring(typeName.IndexOf("<") + 1);
                                typeName = typeName.Substring(0, typeName.IndexOf(">"));
                            }
                            else
                            {
                                typeName = field.DataTypeClass;
                            }

                            switch (typeName)
                            {
                                case "int":
                                case "bool":
                                case "object":
                                case "long":
                                case "string":
                                case "int[]":
                                    continue;
                                case "IeeeAddress":
                                    //code.AppendLine("import " + _packageRootPrefix + "." + typeName + ";");
                                    continue;
                                case "ZclStatus":
                                    //code.AppendLine("import " + packageRootPrefix + packageZcl + ".ZclStatus;");
                                    continue;
                                case "ImageUpgradeStatus":
                                    //code.AppendLine("import " + packageRootPrefix + packageZclField + ".ImageUpgradeStatus;");
                                    continue;
                            }

                            //code.AppendLine("import " + packageRootPrefix + packageName + "." + typeName + ";");
                        }

                        code.AppendLine();
                        code.AppendLine("/**");
                        code.AppendLine(" * " + command.CommandLabel + " value object class.");

                        code.AppendLine(" *");
                        code.AppendLine(" * Cluster: " + cluster.ClusterName + ". Command is sent"
                                + (cluster.Received.ContainsValue(command) ? "TO" : "FROM") + " the server.");
                        code.AppendLine(" * This command is " + ((cluster.ClusterType.Equals("GENERAL"))
                                ? "a generic command used across the profile."
                                : "a specific command used for the " + cluster.ClusterName + " cluster."));

                        if (command.CommandDescription.Count > 0)
                        {
                            code.AppendLine(" *");
                            OutputWithLinebreak(code, "", command.CommandDescription);
                        }

                        code.AppendLine(" *");
                        code.AppendLine(" * Code is auto-generated. Modifications may be overwritten!");

                        code.AppendLine(" */");
                        code.AppendLine();
                        OutputClassGenerated(code);
                        code.AppendLine("namespace ZigBeeNet.ZCL.Clusters." + cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                        code.AppendLine("{");
                        code.AppendLine("   public class " + className + " : ZclCommand");
                        code.AppendLine("   {");

                        foreach (Field field in fields)
                        {
                            code.AppendLine("           /**");
                            code.AppendLine("           * " + field.FieldLabel + " command message field.");
                            if (field.Description.Count != 0)
                            {
                                code.AppendLine("           *");
                                OutputWithLinebreak(code, "         ", field.Description);
                            }
                            code.AppendLine("           */");
                            code.AppendLine("           public " + field.DataTypeClass + " " + field.NameUpperCamelCase + " { get; set; }");
                            code.AppendLine();
                        }

                        // if (fields.size() > 0) {
                        // code.AppendLine(" static {");
                        // for (final Field field : fields) {
                        // code.AppendLine(" fields.put(" + field.fieldId + ", new ZclField(" + field.fieldId
                        // + ", \"" + field.fieldLabel + "\", ZclDataType." + field.dataType + "));");
                        // }
                        // code.AppendLine(" }");
                        // code.AppendLine();
                        // }
                        code.AppendLine();
                        code.AppendLine("           /**");
                        code.AppendLine("           * Default constructor.");
                        code.AppendLine("           */");
                        code.AppendLine("           public " + className + "()");
                        code.AppendLine("           {");
                        // code.AppendLine(" setType(ZclCommandType." + command.commandType + ");");
                        code.AppendLine("               GenericCommand = " + ((cluster.ClusterType.Equals("GENERAL")) ? "true" : "false") + ";");

                        if (!cluster.ClusterType.Equals("GENERAL"))
                        {
                            code.AppendLine("               ClusterId = " + cluster.ClusterId + ";");
                        }

                        code.AppendLine("               CommandId = " + command.CommandId + ";");

                        code.AppendLine("               CommandDirection = ZclCommandDirection."
                                + (cluster.Received.ContainsValue(command) ? "CLIENT_TO_SERVER" : "SERVER_TO_CLIENT")
                                + ";");

                        code.AppendLine("    }");
                        // code.AppendLine();
                        // code.AppendLine(" /**");
                        // code.AppendLine(" * Constructor copying field values from command message.");
                        // code.AppendLine(" *");
                        // code.AppendLine(" * @param fields a {@link Map} containing the value {@link Object}s");
                        // code.AppendLine(" */");
                        // code.AppendLine(" public " + className + "(final Map<Integer, Object> fields) {");
                        // code.AppendLine(" this();");
                        // for (final Field field : fields) {
                        // code.AppendLine(" " + field.nameLowerCamelCase + " = (" + field.dataTypeClass
                        // + ") fields.get(" + field.fieldId + ");");
                        // }
                        // code.AppendLine(" }");
                        // code.AppendLine();
                        // code.AppendLine(" @Override");
                        // code.AppendLine(" public ZclCommandMessage toCommandMessage() {");
                        // code.AppendLine(" final ZclCommandMessage message = super.toCommandMessage();");
                        // for (final Field field : fields) {
                        // code.AppendLine(" message.getFields().put(ZclFieldType." + field.fieldType + ","
                        // + field.nameLowerCamelCase + ");");
                        // }
                        // code.AppendLine(" return message;");
                        // code.AppendLine(" }");

                        //if (cluster.ClusterType.Equals("GENERAL"))
                        //{
                        //    code.AppendLine();
                        //    code.AppendLine("           /**");
                        //    code.AppendLine("            * Sets the cluster ID for generic commands. " + className + " is a generic command.");
                        //    code.AppendLine("            *");
                        //    code.AppendLine("            * For commands that are not generic, this method will do nothing as the cluster ID is fixed.");
                        //    code.AppendLine("            * To test if a command is generic, use the IsGenericCommand method.");
                        //    code.AppendLine("            *");
                        //    code.AppendLine("            * @param clusterId the cluster ID used for <i>generic</i> commands as an {@link Integer}");

                        //    code.AppendLine("            */");
                        //    code.AppendLine("           public override void SetClusterId(int clusterId)");
                        //    code.AppendLine("           {");
                        //    code.AppendLine("               this.clusterId = clusterId;");
                        //    code.AppendLine("           }");
                        //}

                        //foreach (Field field in fields)
                        //{
                        //    code.AppendLine();
                        //    code.AppendLine("    /**");
                        //    code.AppendLine("     * Gets " + field.FieldLabel + ".");
                        //    if (field.Description.Count != 0)
                        //    {
                        //        code.AppendLine("     *");
                        //        foreach (string line in field.Description)
                        //        {
                        //            code.AppendLine("     * " + line);
                        //        }
                        //    }
                        //    code.AppendLine("     *");
                        //    code.AppendLine("     * @return the " + field.FieldLabel);
                        //    code.AppendLine("     */");
                        //    code.AppendLine("    public " + field.DataTypeClass + " Get" + field.NameUpperCamelCase + "()");
                        //    code.AppendLine("    {");
                        //    code.AppendLine("        return " + field.NameLowerCamelCase + ";");
                        //    code.AppendLine("    }");
                        //    code.AppendLine();
                        //    code.AppendLine("    /**");
                        //    code.AppendLine("     * Sets " + field.FieldLabel + ".");
                        //    if (field.Description.Count != 0)
                        //    {
                        //        code.AppendLine("     *");
                        //        OutputWithLinebreak(code, "    ", field.Description);
                        //    }
                        //    code.AppendLine("     *");
                        //    code.AppendLine("     * @param " + field.NameLowerCamelCase + " the " + field.FieldLabel);
                        //    code.AppendLine("     */");
                        //    code.AppendLine("    public void Set" + field.NameUpperCamelCase + field.DataTypeClass + " " + field.NameLowerCamelCase + ")");
                        //    code.AppendLine("    {");
                        //    code.AppendLine("        this." + field.NameLowerCamelCase + " = " + field.NameLowerCamelCase + ";");
                        //    code.AppendLine("    }");

                        //}

                        if (fields.Count > 0)
                        {
                            // code.AppendLine();
                            // code.AppendLine(" @Override");
                            // code.AppendLine(" public void setFieldValues(final Map<Integer, Object> values) {");
                            // for (final Field field : fields) {
                            // code.AppendLine(" " + field.nameLowerCamelCase + " = (" + field.dataTypeClass
                            // + ") values.get(" + field.fieldId + ");");
                            // }
                            // code.AppendLine(" }");

                            code.AppendLine();
                            code.AppendLine("    public override void Serialize(ZclFieldSerializer serializer)");
                            code.AppendLine("    {");
                            foreach (Field field in fields)
                            {
                                // Rules...
                                // if listSizer == null, then just output the field
                                // if listSizer != null and contains && then check the param bit

                                if (field.ListSizer != null)
                                {
                                    if (field.ListSizer.Equals("statusResponse"))
                                    {
                                        // Special case where a ZclStatus may be sent, or, a list of results.
                                        // This checks for a single response
                                        code.AppendLine("        if (Status == ZclStatus.SUCCESS)");
                                        code.AppendLine("        {");
                                        code.AppendLine("            serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));");
                                        code.AppendLine("            return;");
                                        code.AppendLine("        }");
                                    }
                                    else if (field.ConditionOperator != null)
                                    {
                                        if (field.ConditionOperator == "&&")
                                        {
                                            code.AppendLine("        if ((" + field.ListSizer + " & " + field.Condition + ") != 0) {");
                                        }
                                        else
                                        {
                                            code.AppendLine("        if (" + field.ListSizer + " " + field.ConditionOperator + " " + field.Condition + ")");
                                            code.AppendLine("        {");
                                        }
                                        code.AppendLine("            serializer.Serialize(" + field.NameUpperCamelCase + ", ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("        }");
                                    }
                                    else
                                    {
                                        code.AppendLine("        for (int cnt = 0; cnt < " + field.NameUpperCamelCase + ".Count; cnt++) {");
                                        code.AppendLine("            serializer.Serialize(" + field.NameUpperCamelCase + ".Get(cnt), ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("        }");
                                    }
                                }
                                else
                                {
                                    code.AppendLine("        serializer.Serialize(" + field.NameUpperCamelCase + ", ZclDataType.Get(DataType." + field.DataType + "));");
                                }
                            }
                            code.AppendLine("    }");

                            code.AppendLine();
                            code.AppendLine("    public override void Deserialize(ZclFieldDeserializer deserializer)");
                            code.AppendLine("    {");

                            foreach (Field field in fields)
                            {
                                if (field.ListSizer != null)
                                {
                                    if (field.ListSizer.Equals("statusResponse"))
                                    {
                                        // Special case where a ZclStatus may be sent, or, a list of results.
                                        // This checks for a single response
                                        code.AppendLine("        if (deserializer.RemainingLength == 1)");
                                        code.AppendLine("        {");
                                        code.AppendLine("            Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));");
                                        code.AppendLine("            return;");
                                        code.AppendLine("        }");
                                    }
                                    else if (field.ConditionOperator != null)
                                    {
                                        if (field.ConditionOperator == "&&")
                                        {
                                            code.AppendLine("        if ((" + field.ListSizer + " & " + field.Condition + ") != 0)");
                                            code.AppendLine("        {");
                                        }
                                        else
                                        {
                                            code.AppendLine("        if (" + field.ListSizer + " " + field.ConditionOperator + " " + field.Condition + ")");
                                            code.AppendLine("        {");
                                        }
                                        code.AppendLine("            " + field.NameUpperCamelCase + " = deserializer.Deserialize<" + field.DataTypeClass + ">(ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("        }");
                                    }
                                    else
                                    {
                                        code.AppendLine("        for (int cnt = 0; cnt < " + field.NameLowerCamelCase + ".Count; cnt++)");
                                        code.AppendLine("        {");
                                        code.AppendLine("            " + field.NameUpperCamelCase + " = deserializer.Deserialize<" + field.DataTypeClass + ">(ZclDataType.Get(DataType." + field.DataType + "));");
                                        code.AppendLine("        }");
                                    }
                                }
                                else
                                {
                                    code.AppendLine("        " + field.NameUpperCamelCase + " = deserializer.Deserialize<" + field.DataTypeClass + ">(ZclDataType.Get(DataType." + field.DataType + "));");
                                }
                            }
                            code.AppendLine("    }");
                        }

                        int fieldLen = 0;
                        foreach (Field field in fields)
                        {
                            fieldLen += field.NameLowerCamelCase.Length + 20;
                        }

                        code.AppendLine();
                        code.AppendLine("       public override string ToString()");
                        code.AppendLine("       {");
                        code.AppendLine("           var builder = new StringBuilder();");
                        code.AppendLine();
                        code.AppendLine("           builder.Append(\"" + className + " [\");");
                        code.AppendLine("           builder.Append(base.ToString());");
                        foreach (Field field in fields)
                        {
                            code.AppendLine("           builder.Append(\", " + field.NameUpperCamelCase + "=\");");
                            code.AppendLine("           builder.Append(" + field.NameUpperCamelCase + ");");
                        }
                        code.AppendLine("           builder.Append(\']\');");
                        code.AppendLine();
                        code.AppendLine("           return builder.ToString();");
                        code.AppendLine("       }");
                        code.AppendLine();
                        code.AppendLine("   }");
                        code.AppendLine("}");

                        Console.WriteLine(code.ToString());

                        var outputPath = Path.Combine(_outRootPath, cluster.ClusterName.Replace("/", "").Replace(" ", ""));
                        //var outputPath = @"C:\src\ZigbeeNet\src\ZigBeeNet\CodeGenTest\" + cluster.ClusterName.Replace("/", "");
                        var commmandClassFile = command.NameUpperCamelCase + ".cs";
                        var commandFullPath = Path.Combine(outputPath, commmandClassFile.Replace(" ", ""));

                        Directory.CreateDirectory(outputPath);

                        File.Delete(commandFullPath);

                        File.WriteAllText(commandFullPath, code.ToString(), Encoding.UTF8);
                    }
                }
            }
        }

        //private static string getZclCommandTypeEnum(final Cluster cluster, final Command command, string string)
        //{
        //    return command.commandType + "(" + string.format("0x%04X", cluster.clusterId) + ", " + command.commandId + ", "
        //            + command.nameUpperCamelCase + ".class" + ", " + string + ")";
        //    // return command.commandType + "(ZclClusterType." + cluster.clusterType + ", " + command.commandId + ", "
        //    // + command.nameUpperCamelCase + ".class" + ", " + received + ")";
        //}

        //private static void generateZclCommandTypeEnumeration(Context context, string packageRootPrefix,
        //        File sourceRootPath) throws IOException
        //{
        //    final string className = "ZclCommandType";

        //    final string packageRoot = packageRootPrefix + packageZclProtocol;
        //    final string packagePath = getPackagePath(sourceRootPath, packageZclProtocol);
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    CodeGeneratorUtil.outputLicense(out);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();

        //    Console.WriteLine("import java.lang.reflect.Constructor;");
        //    Console.WriteLine();
        //    Console.WriteLine("import javax.annotation.Generated;");
        //    Console.WriteLine();
        //    Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclCommand;");
        //    Console.WriteLine("import " + packageRootPrefix + packageZclProtocol + ".ZclCommandDirection;");
        //    Console.WriteLine();

        //    Map<string, Command> commandEnum = new TreeMap<string, Command>();

        //        final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                // Brute force to get the commands in order!
        //                for (int c = 0; c< 65535; c++) {
        //                    if (cluster.received.get(c) != null) {
        //                        Console.WriteLine("import " + getZclClusterCommandPackage(packageRootPrefix, cluster) + "."
        //                                + cluster.received.get(c).nameUpperCamelCase + ";");

        //commandEnum.put(getZclCommandTypeEnum(cluster, cluster.received.get(c),
        //                                "ZclCommandDirection.CLIENT_TO_SERVER"), cluster.received.get(c));
        //                    }
        //                    if (cluster.generated.get(c) != null) {
        //                        Console.WriteLine("import " + getZclClusterCommandPackage(packageRootPrefix, cluster) + "."
        //                                + cluster.generated.get(c).nameUpperCamelCase + ";");

        //commandEnum.put(getZclCommandTypeEnum(cluster, cluster.generated.get(c),
        //                                "ZclCommandDirection.SERVER_TO_CLIENT"), cluster.generated.get(c));
        //                    }
        //                }
        //            }
        //        }
        //        Console.WriteLine();

        //        Console.WriteLine();
        //outputClassJavaDoc(out, "Enumeration of ZigBee Cluster Library commands");
        //outputClassGenerated(out);
        //Console.WriteLine("public enum " + className + " {");
        //boolean first = true;
        //        for (string command : commandEnum.keySet()) {
        //            Command cmd = commandEnum.get(command);
        //            if (cmd == null) {
        //                Console.WriteLine("Command without data: " + command);
        //                continue;
        //            }

        //            if (!first) {
        //                Console.WriteLine(",");
        //            }
        //            first = false;
        //            Console.WriteLine("    /**");
        //            Console.WriteLine("     * " + cmd.commandType + ": " + cmd.commandLabel);
        //            Console.WriteLine("     * <p>");
        //            Console.WriteLine("     * See {@link " + cmd.nameUpperCamelCase + "}");
        //            Console.WriteLine("     */");
        //            out.print("    " + command);
        //        }
        //        Console.WriteLine(";");
        //        Console.WriteLine();

        //        Console.WriteLine("    private final int commandId;");
        //        Console.WriteLine("    private final int clusterType;");
        //        Console.WriteLine("    private final Class<? extends ZclCommand> commandClass;");
        //        // Console.WriteLine(" private final string label;");
        //        Console.WriteLine("    private final ZclCommandDirection direction;");
        //        Console.WriteLine();
        //        Console.WriteLine("    " + className
        //                + "(final int clusterType, final int commandId, final Class<? extends ZclCommand> commandClass, final ZclCommandDirection direction) {");
        //        Console.WriteLine("        this.clusterType = clusterType;");
        //        Console.WriteLine("        this.commandId = commandId;");
        //        Console.WriteLine("        this.commandClass = commandClass;");
        //        // Console.WriteLine(" this.label = label;");
        //        Console.WriteLine("        this.direction = direction;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();

        //        Console.WriteLine("    public int getClusterType() {");
        //        Console.WriteLine("        return clusterType;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public int getId() {");
        //        Console.WriteLine("        return commandId;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        // Console.WriteLine(" public string getLabel() { return label; }");
        //        Console.WriteLine("    public boolean isGeneric() {");
        //        Console.WriteLine("        return clusterType==0xFFFF;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public ZclCommandDirection getDirection() {");
        //        Console.WriteLine("        return direction;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public Class<? extends ZclCommand> getCommandClass() {");
        //        Console.WriteLine("        return commandClass;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine(
        //                "        public static ZclCommandType getCommandType(final int clusterType, final int commandId,\n");
        //        Console.WriteLine("            ZclCommandDirection direction) {\n");
        //        Console.WriteLine("        for (final ZclCommandType value : values()) {\n");
        //        Console.WriteLine(
        //                "            if (value.direction == direction && value.clusterType == clusterType && value.commandId == commandId) {\n");
        //        Console.WriteLine("                return value;\n");
        //        Console.WriteLine("            }\n");
        //        Console.WriteLine("        }\n");
        //        Console.WriteLine("        return null;\n");
        //        Console.WriteLine("    }");

        //        Console.WriteLine();
        //        Console.WriteLine("    public static ZclCommandType getGeneric(final int commandId) {");
        //        Console.WriteLine("        for (final ZclCommandType value : values()) {");
        //        Console.WriteLine("            if (value.clusterType == 0xFFFF && value.commandId == commandId) {");
        //        Console.WriteLine("                return value;");
        //        Console.WriteLine("            }");
        //        Console.WriteLine("        }");
        //        Console.WriteLine("        return null;");
        //        Console.WriteLine("    }");

        //        Console.WriteLine();
        //        Console.WriteLine("    public ZclCommand instantiateCommand() {");
        //        Console.WriteLine("        Constructor<? extends ZclCommand> cmdConstructor;");
        //        Console.WriteLine("        try {");
        //        Console.WriteLine("            cmdConstructor = commandClass.getConstructor();");
        //        Console.WriteLine("            return cmdConstructor.newInstance();");
        //        Console.WriteLine("        } catch (Exception e) {");
        //        Console.WriteLine("            // logger.debug(\"Error instantiating cluster command {}\", this);");
        //        Console.WriteLine("        }");
        //        Console.WriteLine("        return null;");
        //        Console.WriteLine("    }");

        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        private static void OutputWithLinebreak(StringBuilder builder, string indent, List<string> lines)
        {
            foreach (string line in lines)
            {
                string[] words = line.Split(" ");
                if (words.Length == 0)
                {
                    return;
                }

                builder.Append(indent + " *");

                int len = 2;
                foreach (string word in words)
                {
                    // if (word.toLowerCase().equals("note:")) {
                    // if (len > 2) {
                    // Console.WriteLine();
                    // }
                    // Console.WriteLine(indent + " * <p>");
                    // out.print(indent + " * <b>Note:</b>");
                    // continue;
                    // }
                    if (len + word.Length > _lineLen)
                    {
                        builder.AppendLine();
                        builder.Append(indent + " *");
                        len = 2;
                    }
                    builder.Append(" ");
                    builder.Append(word);
                    len += word.Length;
                }

                if (len != 0)
                {
                    Console.WriteLine();
                }
            }
        }

        private static void GenerateZclClusterClasses(Context context/*, string packageRootPrefix, File sourceRootPath*/)
        {

            LinkedList<Profile> profiles = new LinkedList<Profile>(context.Profiles.Values);
            foreach (Profile profile in profiles)
            {
                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);
                foreach (Cluster cluster in clusters)
                {
                    string packageRoot = "ZigBeeNet";
                    string className = "Zcl" + cluster.NameUpperCamelCase + "Cluster";

                    //string packagePath = getPackagePath(sourceRootPath, packageRoot);
                    //FileInfo packageFile = getPackageFile(packagePath + (packageZclCluster).replace('.', '/'));

                    //final string className = "Zcl" + cluster.nameUpperCamelCase + "Cluster";
                    //final PrintWriter out = getClassOut(packageFile, className);

                    List<Command> commands = new List<Command>();
                    commands.AddRange(cluster.Received.Values);
                    commands.AddRange(cluster.Generated.Values);

                    var code = new StringBuilder();

                    CodeGeneratorUtil.OutputLicense(code);

                    code.AppendLine();

                    code.AppendLine("using System;");
                    code.AppendLine("using System.Collections.Concurrent;");
                    code.AppendLine("using System.Collections.Generic;");
                    code.AppendLine("using System.Linq;");
                    code.AppendLine("using System.Text;");
                    code.AppendLine("using System.Threading;");
                    code.AppendLine("using System.Threading.Tasks;");
                    code.AppendLine("using ZigBeeNet.DAO;");
                    code.AppendLine("using ZigBeeNet.ZCL.Protocol;");
                    code.AppendLine("using ZigBeeNet.ZCL.Field;");

                    if (commands.Count > 0)
                        code.AppendLine("using ZigBeeNet.ZCL.Clusters." + cluster.ClusterName.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + ";");

                    List<string> imports = new List<string>();

                    bool useList = false;
                    foreach (Command command in commands)
                    {
                        List<Field> fields = new List<Field>(command.Fields.Values);
                        Console.WriteLine("Checking command " + command.CommandLabel);

                        foreach (Field field in fields)
                        {
                            Console.WriteLine("Checking " + field.DataTypeClass);
                            string packageName;
                            //if (field.dataTypeClass.contains("Descriptor"))
                            //{
                            //    packageName = packageZdpDescriptors;
                            //}
                            //else
                            //{
                            packageName = _packageZclField;
                            //}

                            string typeName;
                            if (field.DataTypeClass.StartsWith("List"))
                            {
                                useList = true;
                                typeName = field.DataTypeClass;
                                typeName = typeName.Substring(typeName.IndexOf("<") + 1);
                                typeName = typeName.Substring(0, typeName.IndexOf(">"));
                            }
                            else
                            {
                                typeName = field.DataTypeClass;
                            }

                            switch (typeName)
                            {
                                case "int":
                                case "bool":
                                case "object":
                                case "long":
                                case "string":
                                case "int[]":
                                    continue;
                                case "IeeeAddress":
                                    //imports.Add(packageRootPrefix + "." + typeName);
                                    Console.WriteLine("Adding " + typeName);
                                    continue;
                                case "ZclStatus":
                                    //imports.add(packageRootPrefix + packageZcl + ".ZclStatus");
                                    continue;
                                case "ImageUpgradeStatus":
                                    //imports.add(packageRootPrefix + packageZclField + ".ImageUpgradeStatus");
                                    continue;
                            }

                            //imports.Add(packageRootPrefix + packageName + "." + typeName);
                        }
                    }

                    if (useList)
                    {
                        //imports.add("java.util.List");
                        // imports.add(packageRootPrefix + packageZclField + ".*");
                    }

                    bool addAttributeTypes = false;
                    bool readAttributes = false;
                    bool writeAttributes = false;

                    foreach (Attribute attribute in cluster.Attributes.Values)
                    {
                        if (attribute.AttributeAccess.ToLower().Contains("write"))
                        {
                            addAttributeTypes = true;
                            writeAttributes = true;
                        }
                        if (attribute.AttributeAccess.ToLower().Contains("read"))
                        {
                            readAttributes = true;
                        }

                        if ("Calendar".Equals(attribute.DataTypeClass))
                        {
                            //imports.add("java.util.Calendar");
                        }
                        if ("IeeeAddress".Equals(attribute.DataTypeClass))
                        {
                            //imports.add("com.zsmartsystems.zigbee.IeeeAddress");
                        }
                        if ("ImageUpgradeStatus".Equals(attribute.DataTypeClass))
                        {
                            //imports.add(packageRootPrefix + packageZclField + ".ImageUpgradeStatus");
                        }
                    }

                    if (addAttributeTypes)
                    {
                        //imports.add("com.zsmartsystems.zigbee.zcl.protocol.ZclDataType");
                    }

                    //imports.Add(packageRoot + _packageZcl + ".ZclCluster");
                    if (cluster.Attributes.Count != 0)
                    {
                        //imports.Add(packageRoot + _packageZclProtocol + ".ZclDataType");
                    }

                    if (commands.Count != 0)
                    {
                        //imports.Add(packageRoot + _packageZcl + ".ZclCommand");
                    }
                    // imports.add(packageRoot + packageZcl + ".ZclCommandMessage");
                    //imports.Add("javax.annotation.Generated");

                    // imports.add(packageRoot + ".ZigBeeDestination");
                    //imports.Add(packageRoot + ".ZigBeeEndpoint");

                    if (cluster.Attributes.Count != 0 || commands.Count != 0)
                    {
                        //imports.Add(packageRoot + ".CommandResult");
                    }

                    //imports.Add(packageRoot + _packageZcl + ".ZclAttribute");

                    if (cluster.Attributes.Count != 0 || commands.Count != 0)
                    {
                        //imports.Add("java.util.concurrent.Future");
                    }
                    // imports.add("com.zsmartsystems.zigbee.model.ZigBeeType");

                    foreach (Attribute attribute in cluster.Attributes.Values)
                    {
                        if (attribute.AttributeAccess.ToLower().Contains("read"))
                        {
                            // imports.add("java.util.Calendar");
                        }
                    }

                    foreach (Command command in commands)
                    {
                        //imports.Add(GetZclClusterCommandPackage(packageRoot, cluster));
                    }

                    if (cluster.Attributes.Count != 0)
                    {
                        //imports.Add(packageRoot + _packageZclProtocol + ".ZclClusterType");
                    }

                    List<string> importList = new List<string>();
                    importList.AddRange(imports);
                    importList = importList.Distinct().ToList();
                    importList.Sort();

                    foreach (string importClass in importList)
                    {
                        code.AppendLine("using " + importClass + ";");
                    }

                    code.AppendLine();
                    code.AppendLine("/**");
                    code.AppendLine(" *" + cluster.ClusterName + "cluster implementation (Cluster ID 0x" + cluster.ClusterId.ToString("X4") + ").");

                    if (cluster.ClusterDescription.Count > 0)
                    {
                        code.AppendLine(" *");
                    }

                    OutputWithLinebreak(code, "", cluster.ClusterDescription);

                    code.AppendLine(" *");
                    code.AppendLine(" * Code is auto-generated. Modifications may be overwritten!");

                    code.AppendLine(" */");
                    // outputClassJavaDoc(out);
                    OutputClassGenerated(code);

                    code.AppendLine("namespace ZigBeeNet.ZCL.Clusters");
                    code.AppendLine("{");
                    code.AppendLine("   public class " + className + " : ZclCluster");
                    code.AppendLine("   {");
                    code.AppendLine("       /**");
                    code.AppendLine("       * The ZigBee Cluster Library Cluster ID");
                    code.AppendLine("       */");
                    code.AppendLine("       public static ushort CLUSTER_ID = 0x" + cluster.ClusterId.ToString("X4") + ";");
                    code.AppendLine();
                    code.AppendLine("       /**");
                    code.AppendLine("       * The ZigBee Cluster Library Cluster Name");
                    code.AppendLine("       */");
                    code.AppendLine("       public static string CLUSTER_NAME = \"" + cluster.ClusterName + "\";");
                    code.AppendLine();

                    if (cluster.Attributes.Count != 0)
                    {
                        code.AppendLine("       /* Attribute constants */");
                        foreach (Attribute attribute in cluster.Attributes.Values)
                        {
                            code.AppendLine("       /**");
                            OutputWithLinebreak(code, "       ", attribute.AttributeDescription);
                            code.AppendLine("       */");
                            code.AppendLine("       public static ushort " + attribute.EnumName + " = 0x" + attribute.AttributeId.ToString("X4") + ";");
                            code.AppendLine();
                        }

                        code.AppendLine();
                    }

                    code.AppendLine("       // Attribute initialisation");
                    code.AppendLine("       protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()");
                    code.AppendLine("       {");
                    code.AppendLine("           Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(" + cluster.Attributes.Count + ");");

                    if (cluster.Attributes.Count != 0)
                    {
                        code.AppendLine();
                        code.AppendLine("           ZclClusterType " + cluster.NameLowerCamelCase.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + " = ZclClusterType.GetValueById(ClusterType." + cluster.ClusterType + ");");
                        code.AppendLine();

                        foreach (Attribute attribute in cluster.Attributes.Values)
                        {
                            code.AppendLine("           attributeMap.Add(" + attribute.EnumName
                                    + ", new ZclAttribute(" + cluster.NameLowerCamelCase.Replace("/", "").Replace(" ", "").Replace("(", "").Replace(")", "") + ", " + attribute.EnumName
                                    + ", \"" + attribute.AttributeLabel + "\", " + "ZclDataType.Get(DataType." + attribute.DataType + ")"
                                    + ", " + "mandatory".Equals(attribute.AttributeImplementation.ToLower()).ToString().ToLower() + ", "
                                    + attribute.AttributeAccess.ToLower().Contains("read").ToString().ToLower() + ", "
                                    + attribute.AttributeAccess.ToLower().Contains("write").ToString().ToLower() + ", "
                                    + "mandatory".Equals(attribute.AttributeReporting.ToLower()).ToString().ToLower() + "));");
                        }
                    }

                    code.AppendLine();
                    code.AppendLine("        return attributeMap;");
                    code.AppendLine("       }");
                    code.AppendLine();

                    code.AppendLine("       /**");
                    code.AppendLine("       * Default constructor to create a " + cluster.ClusterName + " cluster.");
                    code.AppendLine("       *");
                    code.AppendLine("       * @param zigbeeEndpoint the {@link ZigBeeEndpoint}");
                    code.AppendLine("       */");
                    code.AppendLine("       public " + className + "(ZigBeeEndpoint zigbeeEndpoint)");
                    code.AppendLine("           : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)");
                    code.AppendLine("       {");
                    code.AppendLine("       }");
                    code.AppendLine();

                    foreach (Attribute attribute in cluster.Attributes.Values)
                    {
                        DataTypeMap zclDataType = ZclDataType.GetDataTypeMapping()[attribute.DataType];

                        if (attribute.AttributeAccess.ToLower().Contains("write"))
                        {
                            OutputAttributeJavaDoc(code, "Set", attribute, zclDataType);
                            code.AppendLine("       public Task<CommandResult> Set" + attribute.NameUpperCamelCase.Replace("_", "") + "(object value)");
                            code.AppendLine("       {");
                            code.AppendLine("           return Write(_attributes[" + attribute.EnumName + "], value);");
                            code.AppendLine("       }");
                            code.AppendLine();
                        }

                        if (attribute.AttributeAccess.ToLower().Contains("read"))
                        {
                            OutputAttributeJavaDoc(code, "Get", attribute, zclDataType);
                            code.AppendLine("       public Task<CommandResult> Get" + attribute.NameUpperCamelCase.Replace("_", "") + "Async()");
                            code.AppendLine("       {");
                            code.AppendLine("           return Read(_attributes[" + attribute.EnumName + "]);");
                            code.AppendLine("       }");
                            OutputAttributeJavaDoc(code, "Synchronously Get", attribute, zclDataType);
                            code.AppendLine("       public " + attribute.DataTypeClass + " Get" + attribute.NameUpperCamelCase.Replace("_", "") + "(long refreshPeriod)");
                            code.AppendLine("       {");
                            code.AppendLine("           if (_attributes[" + attribute.EnumName + "].IsLastValueCurrent(refreshPeriod))");
                            code.AppendLine("           {");
                            code.AppendLine("               return (" + attribute.DataTypeClass + ")_attributes[" + attribute.EnumName + "].LastValue;");
                            code.AppendLine("           }");
                            code.AppendLine();
                            code.AppendLine("           return (" + attribute.DataTypeClass + ")ReadSync(_attributes[" + attribute.EnumName + "]);");
                            code.AppendLine("       }");
                            code.AppendLine();
                        }

                        if (attribute.AttributeAccess.ToLower().Contains("read") && attribute.AttributeReporting.ToLower().Equals("mandatory"))
                        {
                            OutputAttributeJavaDoc(code, "Set reporting for", attribute, zclDataType);
                            if (zclDataType.Analogue)
                            {
                                code.AppendLine("       public Task<CommandResult> Set" + attribute.NameUpperCamelCase + "Reporting(ushort minInterval, ushort maxInterval, object reportableChange)");
                                code.AppendLine("       {");
                                code.AppendLine("           return SetReporting(_attributes[" + attribute.EnumName + "], minInterval, maxInterval, reportableChange);");
                            }
                            else
                            {
                                code.AppendLine("       public Task<CommandResult> Set" + attribute.NameUpperCamelCase + "Reporting(ushort minInterval, ushort maxInterval)");
                                code.AppendLine("       {");
                                code.AppendLine("           return SetReporting(_attributes[" + attribute.EnumName + "], minInterval, maxInterval);");
                            }
                            code.AppendLine("       }");
                            code.AppendLine();
                        }
                    }

                    foreach (Command command in commands)
                    {
                        code.AppendLine();
                        code.AppendLine("       /**");
                        code.AppendLine("       * The " + command.CommandLabel);

                        if (command.CommandDescription.Count != 0)
                        {
                            code.AppendLine("       *");
                            OutputWithLinebreak(code, "       ", command.CommandDescription);
                        }

                        code.AppendLine("       *");

                        List<Field> fields = new List<Field>(command.Fields.Values);

                        foreach (Field field in fields)
                        {
                            code.AppendLine("       * @param " + field.NameLowerCamelCase + " {@link " + field.DataTypeClass + "} " + field.FieldLabel);
                        }

                        code.AppendLine("       * @return the Task<CommandResult> command result Task");
                        code.AppendLine("       */");
                        code.Append("       public Task<CommandResult> " + command.NameUpperCamelCase + "(");

                        bool first = true;

                        foreach (Field field in fields)
                        {
                            if (first == false)
                            {
                                code.Append(", ");
                            }

                            code.Append(field.DataTypeClass + " " + field.NameLowerCamelCase);
                            first = false;
                        }

                        code.AppendLine(")");
                        code.AppendLine("       {");
                        code.AppendLine("           " + command.NameUpperCamelCase + " command = new " + command.NameUpperCamelCase + "();");

                        if (fields.Count != 0)
                        {
                            code.AppendLine();
                            code.AppendLine("       // Set the fields");
                        }

                        foreach (Field field in fields)
                        {
                            code.AppendLine("           command." + field.NameUpperCamelCase + " = " + field.NameLowerCamelCase + ";");
                        }

                        code.AppendLine();
                        code.AppendLine("           return Send(command);");
                        code.AppendLine("       }");
                    }

                    // if (readAttributes) {
                    // code.AppendLine();
                    // code.AppendLine(" /**");
                    // code.AppendLine(" * Add a binding for this cluster to the local node");
                    // code.AppendLine(" *");
                    // code.AppendLine(" * @return the {@link Future<CommandResult>} command result future");
                    // code.AppendLine(" */");
                    // code.AppendLine(" public Future<CommandResult> bind() {");
                    // code.AppendLine(" return bind();");
                    // code.AppendLine(" }");
                    // }

                    if (cluster.Received.Count > 0)
                    {
                        code.AppendLine();
                        code.AppendLine("       public override ZclCommand GetCommandFromId(int commandId)");
                        code.AppendLine("       {");
                        code.AppendLine("           switch (commandId)");
                        code.AppendLine("           {");

                        foreach (Command command in cluster.Received.Values)
                        {
                            code.AppendLine("               case " + command.CommandId + ": // " + command.CommandType);
                            code.AppendLine("                   return new " + command.NameUpperCamelCase + "();");
                        }

                        code.AppendLine("                   default:");
                        code.AppendLine("                       return null;");
                        code.AppendLine("           }");
                        code.AppendLine("       }");
                    }

                    if (cluster.Generated.Count > 0)
                    {
                        code.AppendLine();
                        code.AppendLine("       public ZclCommand getResponseFromId(int commandId)");
                        code.AppendLine("       {");
                        code.AppendLine("           switch (commandId)");
                        code.AppendLine("           {");

                        foreach (Command command in cluster.Generated.Values)
                        {
                            code.AppendLine("               case " + command.CommandId + ": // " + command.CommandType);
                            code.AppendLine("                   return new " + command.NameUpperCamelCase + "();");
                        }

                        code.AppendLine("                   default:");
                        code.AppendLine("                       return null;");
                        code.AppendLine("           }");
                        code.AppendLine("       }");
                    }
                    code.AppendLine("   }");

                    code.AppendLine("}");

                    Console.WriteLine(code.ToString());

                    //var outputPath = Path.Combine("..", "..", "..", "GenerationResults");
                    //var outputPath = @"C:\src\ZigbeeNet\src\ZigBeeNet\CodeGenTest\";
                    var commandClassFile = "Zcl" + cluster.NameUpperCamelCase + "Cluster.cs";
                    var clusterFullPath = Path.Combine(_outRootPath, commandClassFile);

                    Directory.CreateDirectory(_outRootPath);

                    File.Delete(clusterFullPath);

                    File.WriteAllText(clusterFullPath, code.ToString(), Encoding.UTF8);
                }
            }
        }

        private static void generateAttributeEnumeration(Context context)
        {

            List<Profile> profiles = new List<Profile>(context.Profiles.Values);

            foreach (Profile profile in profiles)
            {
                List<Cluster> clusters = new List<Cluster>(profile.Clusters.Values);
                foreach (Cluster cluster in clusters)
                {
                    if (cluster.Attributes.Count != 0)
                    {
                        foreach (Attribute attribute in cluster.Attributes.Values)
                        {
                            if (attribute.ValueMap.Count == 0)
                            {
                                continue;
                            }

                            string packageRoot = "ZigBeeNet.ZCL.Clusters." + cluster.ClusterType.Replace("_", "").ToLower();

                            string className = attribute.NameUpperCamelCase + "Enum";

                            outputEnum(packageRoot, className, attribute.ValueMap, cluster.ClusterName, attribute.AttributeLabel);
                        }
                    }
                }
            }
        }

        //        private static void generateFieldEnumeration(Context context, string packageRootPrefix, File sourceRootPath)
        //                    throws IOException
        //        {

        //            final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //                for (final Profile profile : profiles) {
        //                    final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //                    for (final Cluster cluster : clusters) {
        //                        final ArrayList<Command> commands = new ArrayList<Command>();
        //        commands.addAll(cluster.received.values());
        //                        commands.addAll(cluster.generated.values());

        //                        if (commands.size() != 0) {
        //                            for (final Command command : commands) {
        //                                for (final Field field : command.fields.values()) {
        //                                    if (field.valueMap.isEmpty()) {
        //                                        continue;
        //                                    }

        //                                    final string packageRoot = packageRootPrefix + packageZclProtocolCommand + "."
        //                                            + cluster.clusterType.replace("_", "").toLowerCase();

        //        final string className = field.nameUpperCamelCase + "Enum";

        //        outputEnum(packageRoot, sourceRootPath, className, field.valueMap, cluster.clusterName,
        //                field.fieldLabel);
        //    }
        //}
        //                        }
        //                    }
        //                }
        //            }

        private static void OutputEnum(string packageRoot, string className, Dictionary<int, string> valueMap, string parentName, string label)
        {

            //final string packagePath = getPackagePath(sourceRootPath, packageRoot);
            //final File packageFile = getPackageFile(packagePath);

            //final PrintWriter out = getClassOut(packageFile, className);
            var code = new StringBuilder();
            CodeGeneratorUtil.OutputLicense(code);

            code.AppendLine("package " + packageRoot + ";");
            /*
             using System;
using System.Collections.Generic;
using System.Text;
             */
            code.AppendLine();
            code.AppendLine("using System;");
            code.AppendLine("using System.Collections.Generic;");
            code.AppendLine("using System.Text;");
            code.AppendLine();
            ö
            OutputClassDoc(code, "Enumeration of " + parentName + " attribute " + label + " options.");
            OutputClassGenerated(code);

            code.AppendLine();
            code.AppendLine("public enum " + className + " {");
            boolean first = true;
            for (final Integer key : valueMap.keySet())
            {
                string value = valueMap.get(key);

                if (!first)
                {
                    code.AppendLine(",");
                }
                first = false;
                    // code.AppendLine(" /**");
                    // code.AppendLine(" * " + cmd.commandLabel);
                    // code.AppendLine(" * <p>");
                    // code.AppendLine(" * See {@link " + cmd.nameUpperCamelCase + "}");
                    // code.AppendLine(" */");
                    out.print("    " + CodeGeneratorUtil.labelToEnumerationValue(value) + string.format("(0x%04X)", key));
            }
            code.AppendLine(";");
            code.AppendLine();

            code.AppendLine("    /**");
            code.AppendLine("     * A mapping between the integer code and its corresponding " + className
                                + " type to facilitate lookup by value.");
            code.AppendLine("     */");
            code.AppendLine("    private static Map<Integer, " + className + "> idMap;");
            code.AppendLine();
            code.AppendLine("    static {");
            code.AppendLine("        idMap = new HashMap<Integer, " + className + ">();");
            code.AppendLine("        for (" + className + " enumValue : values()) {");
            code.AppendLine("            idMap.put(enumValue.key, enumValue);");
            code.AppendLine("        }");
            code.AppendLine("    }");
            code.AppendLine();
            code.AppendLine("    private final int key;");
            code.AppendLine();
            code.AppendLine("    " + className + "(final int key) {");
            code.AppendLine("        this.key = key;");
            code.AppendLine("    }");
            code.AppendLine();

            code.AppendLine("    public int getKey() {");
            code.AppendLine("        return key;");
            code.AppendLine("    }");
            code.AppendLine();
            code.AppendLine("    public static " + className + " getByValue(final int value) {");
            code.AppendLine("        return idMap.get(value);");
            code.AppendLine("    }");
            code.AppendLine("}");

                out.flush();
                out.close();
        }

        private static void OutputAttributeJavaDoc(StringBuilder builder, string type, Attribute attribute, DataTypeMap zclDataType)
        {
            //Console.WriteLine();
            //Console.WriteLine("    /**");
            //Console.WriteLine("     * " + type + " the <i>" + attribute.attributeLabel + "</i> attribute [attribute ID <b>"
            //        + attribute.attributeId + "</b>].");
            //if (attribute.attributeDescription.size() != 0)
            //{
            //    Console.WriteLine("     * <p>");
            //    outputWithLinebreak(out, "    ", attribute.attributeDescription);
            //}
            //if ("Synchronously get".equals(type))
            //{
            //    Console.WriteLine("     * <p>");
            //    Console.WriteLine("     * This method can return cached data if the attribute has already been received.");
            //    Console.WriteLine(
            //            "     * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received");
            //    Console.WriteLine(
            //            "     * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value");
            //    Console.WriteLine(
            //            "     * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.");
            //    Console.WriteLine("     * <p>");
            //    Console.WriteLine(
            //            "     * This method will block until the response is received or a timeout occurs unless the current value is returned.");
            //}
            //Console.WriteLine("     * <p>");
            //Console.WriteLine("     * The attribute is of type {@link " + attribute.dataTypeClass + "}.");
            //Console.WriteLine("     * <p>");
            //Console.WriteLine("     * The implementation of this attribute by a device is "
            //        + attribute.attributeImplementation.toUpperCase());
            //Console.WriteLine("     *");
            //if ("Set reporting for".equals(type))
            //{
            //    Console.WriteLine("     * @param minInterval {@link int} minimum reporting period");
            //    Console.WriteLine("     * @param maxInterval {@link int} maximum reporting period");
            //    if (zclDataType.analogue)
            //    {
            //        Console.WriteLine("     * @param reportableChange {@link Object} delta required to trigger report");
            //    }
            //}
            //else if ("Set".equals(type))
            //{
            //    Console.WriteLine("     * @param " + attribute.nameLowerCamelCase + " the {@link " + attribute.dataTypeClass
            //            + "} attribute value to be set");
            //}

            //if ("Synchronously get".equals(type))
            //{
            //    Console.WriteLine(
            //            "     * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed");
            //    Console.WriteLine("     * @return the {@link " + attribute.dataTypeClass + "} attribute value, or null on error");
            //}
            //else
            //{
            //    Console.WriteLine("     * @return the {@link Future<CommandResult>} command result future");
            //}
            //Console.WriteLine("     */");
        }

        //private static PrintWriter getClassOut(File packageFile, string className) throws FileNotFoundException
        //{
        //    final File classFile = new File(packageFile + File.separator + className + ".java");
        //Console.WriteLine("Generating: " + classFile.getAbsolutePath());
        //final FileOutputStream fileOutputStream = new FileOutputStream(classFile, false);
        //        return new PrintWriter(fileOutputStream);
        //    }

        //    public static List<string> splitstring(string msg, int lineSize)
        //{
        //    List<string> res = new ArrayList<string>();

        //    Pattern p = Pattern.compile("\\b.{1," + (lineSize - 1) + "}\\b\\W?");
        //    Matcher m = p.matcher(msg);

        //    while (m.find())
        //    {
        //        Console.WriteLine(m.group().trim()); // Debug
        //        res.add(m.group());
        //    }
        //    return res;
        //}

        private static string GetZclClusterCommandPackage(string packageRoot, Cluster cluster)
        {
            return packageRoot + _packageZclProtocolCommand + "." + CodeGeneratorUtil.LabelToUpperCamelCase(cluster.ClusterType.Replace("_", "").ToLower());
        }

        //private static string getFieldType(Field field)
        //{
        //    if (field.listSizer != null)
        //    {
        //        return "List<" + field.dataTypeClass + ">";
        //    }
        //    else
        //    {
        //        return field.dataTypeClass;
        //    }
        //}

        //private static void generateZdpCommandClasses(Context context, string packageRootPrefix, File sourceRootPath)
        //            throws IOException
        //{

        //    // List of fields that are handled internally by super class
        //    List<string> reservedFields = new ArrayList<string>();
        //        reservedFields.add("status");

        //        final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                final ArrayList<Command> commands = new ArrayList<Command>();
        //commands.addAll(cluster.received.values());
        //                commands.addAll(cluster.generated.values());
        //                for (final Command command : commands) {
        //                    final string packageRoot = packageRootPrefix + packageZdpCommand;
        //final string packagePath = getPackagePath(sourceRootPath, packageRoot);
        //final File packageFile = getPackageFile(packagePath);

        //final string className = command.nameUpperCamelCase;
        //final PrintWriter out = getClassOut(packageFile, className);

        //final LinkedList<Field> fields = new LinkedList<Field>(command.fields.values());
        //boolean fieldWithDataTypeList = false;
        //                    for (final Field field : fields) {
        //                        if (field.listSizer != null) {
        //                            fieldWithDataTypeList = true;
        //                            break;
        //                        }

        //                        if (field.dataTypeClass.startsWith("List")) {
        //                            fieldWithDataTypeList = true;
        //                            break;
        //                        }
        //                    }

        //                    CodeGeneratorUtil.outputLicense(out);

        //                    Console.WriteLine("package " + packageRoot + ";");
        //                    Console.WriteLine();
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclCommandMessage;");
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZclCommand;");
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclField;");
        //                    if (fields.size() > 0) {
        //                        Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclFieldSerializer;");
        //                        Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclFieldDeserializer;");
        //                        Console.WriteLine("import " + packageRootPrefix + packageZclProtocol + ".ZclDataType;");
        //                    }

        //                    if (className.endsWith("Request")) {
        //                        Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZdoRequest;");
        //                    } else {
        //                        Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZdoResponse;");
        //                    }

        //                    if (command.responseCommand != null && command.responseCommand.length() != 0) {
        //                        Console.WriteLine("import " + packageRootPrefix + ".ZigBeeCommand;");
        //                        Console.WriteLine("import " + packageRootPrefix + packageTransactionPrefix
        //                                + ".ZigBeeTransactionMatcher;");
        //                        Console.WriteLine("import " + packageRootPrefix + packageZdpCommand + "." + command.responseCommand
        //                                + ";");
        //                    }

        //                    if (fieldWithDataTypeList) {
        //                        Console.WriteLine();
        //                        Console.WriteLine("import java.util.List;");
        //                        Console.WriteLine("import java.util.ArrayList;");
        //                    }

        //                    Console.WriteLine("import javax.annotation.Generated;");

        //                    // Console.WriteLine("import java.util.Map;");
        //                    // Console.WriteLine("import java.util.HashMap;");

        //                    for (final Field field : fields) {
        //                        string packageName;
        //                        if (field.dataTypeClass.endsWith("Descriptor")) {
        //                            packageName = packageZdpDescriptors;
        //                        } else if (field.dataTypeClass.endsWith("Table")) {
        //                            packageName = packageZdpDescriptors;
        //                        } else {
        //                            packageName = packageZclField;
        //                        }

        //                        string typeName;
        //                        if (field.dataTypeClass.startsWith("List")) {
        //                            typeName = field.dataTypeClass;
        //                            typeName = typeName.substring(typeName.indexOf("<") + 1);
        //                            typeName = typeName.substring(0, typeName.indexOf(">"));
        //                        } else {
        //                            typeName = field.dataTypeClass;
        //                        }

        //                        // if (reservedFields.contains(field.nameLowerCamelCase)) {
        //                        // continue;
        //                        // }

        //                        switch (typeName) {
        //                            case "Integer":
        //                            case "Boolean":
        //                            case "Object":
        //                            case "Long":
        //                            case "string":
        //                                continue;
        //                            case "IeeeAddress":
        //                                Console.WriteLine("import " + packageRootPrefix + "." + typeName + ";");
        //                                continue;
        //                            case "ZdoStatus":
        //                                Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZdoStatus;");
        //                                continue;
        //                            case "BindingTable":
        //                                Console.WriteLine("import " + packageRootPrefix + packageZdpField + ".BindingTable;");
        //                                continue;
        //                            case "NeighborTable":
        //                                Console.WriteLine("import " + packageRootPrefix + packageZdpField + ".NeighborTable;");
        //                                continue;
        //                        }

        //                        Console.WriteLine("import " + packageRootPrefix + packageName + "." + typeName + ";");
        //                    }

        //                    Console.WriteLine();
        //                    Console.WriteLine("/**");
        //                    Console.WriteLine(" * " + command.commandLabel + " value object class.");

        //                    if (command.commandDescription != null && command.commandDescription.size() != 0) {
        //                        Console.WriteLine(" * <p>");
        //outputWithLinebreak(out, "", command.commandDescription);
        //                    }

        //                    if (cluster.clusterDescription.size() > 0) {
        //                        Console.WriteLine(" * <p>");
        //outputWithLinebreak(out, "", cluster.clusterDescription);
        //                    }

        //                    Console.WriteLine(" * <p>");
        //                    Console.WriteLine(" * Code is auto-generated. Modifications may be overwritten!");

        //                    Console.WriteLine(" */");
        //                    Console.WriteLine();

        //outputClassGenerated(out);

        //                    if (className.endsWith("Request")) {
        //                        out.print("public class " + className + " extends ZdoRequest");
        //                    } else {
        //                        out.print("public class " + className + " extends ZdoResponse");
        //                    }

        //                    if (command.responseCommand != null && command.responseCommand.length() != 0) {
        //                        out.print(" implements ZigBeeTransactionMatcher");
        //                    }
        //                    Console.WriteLine(" {");

        //                    for (final Field field : fields) {
        //                        if (reservedFields.contains(field.nameLowerCamelCase)) {
        //                            continue;
        //                        }

        //                        if (getAutoSized(fields, field.nameLowerCamelCase) != null) {
        //                            continue;
        //                        }

        //                        Console.WriteLine("    /**");
        //                        Console.WriteLine("     * " + field.fieldLabel + " command message field.");
        //                        if (field.description.size() > 0) {
        //                            Console.WriteLine("     * <p>");
        //outputWithLinebreak(out, "    ", field.description);
        //                        }
        //                        Console.WriteLine("     */");
        //                        Console.WriteLine("    private " + getFieldType(field) + " " + field.nameLowerCamelCase + ";");
        //                        Console.WriteLine();
        //                    }

        //                    // if (fields.size() > 0) {
        //                    // Console.WriteLine(" static {");
        //                    // for (final Field field : fields) {
        //                    // Console.WriteLine(" fields.put(" + field.fieldId + ", new ZclField(" + field.fieldId
        //                    // + ", \"" + field.fieldLabel + "\", ZclDataType." + field.dataType + "));");
        //                    // }
        //                    // Console.WriteLine(" }");
        //                    // Console.WriteLine();
        //                    // }

        //                    Console.WriteLine("    /**");
        //                    Console.WriteLine("     * Default constructor.");
        //                    Console.WriteLine("     */");
        //                    Console.WriteLine("    public " + className + "() {");
        //                    // Console.WriteLine(" setType(ZclCommandType." + command.commandType + ");");
        //                    // Console.WriteLine(" commandId = " + command.commandId + ";");
        //                    Console.WriteLine("        clusterId = " + string.format("0x%04X", command.commandId) + ";");
        //                    // Console.WriteLine(" commandDirection = "
        //                    // + (cluster.received.containsValue(command) ? "true" : "false") + ";");

        //                    Console.WriteLine("    }");

        //                    for (final Field field : fields) {
        //                        if (reservedFields.contains(field.nameLowerCamelCase)) {
        //                            continue;
        //                        }

        //                        if (getAutoSized(fields, field.nameLowerCamelCase) != null) {
        //                            continue;
        //                        }

        //                        Console.WriteLine();
        //                        Console.WriteLine("    /**");
        //                        Console.WriteLine("     * Gets " + field.fieldLabel + ".");
        //                        if (field.description.size() != 0) {
        //                            Console.WriteLine("     * <p>");
        //outputWithLinebreak(out, "    ", field.description);
        //                        }
        //                        Console.WriteLine("     *");
        //                        Console.WriteLine("     * @return the " + field.fieldLabel);
        //                        Console.WriteLine("     */");
        //                        Console.WriteLine("    public " + getFieldType(field) + " get" + field.nameUpperCamelCase + "() {");
        //                        Console.WriteLine("        return " + field.nameLowerCamelCase + ";");
        //                        Console.WriteLine("    }");
        //                        Console.WriteLine();
        //                        Console.WriteLine("    /**");
        //                        Console.WriteLine("     * Sets " + field.fieldLabel + ".");
        //                        if (field.description.size() != 0) {
        //                            Console.WriteLine("     * <p>");
        //outputWithLinebreak(out, "    ", field.description);
        //                        }
        //                        Console.WriteLine("     *");
        //                        Console.WriteLine("     * @param " + field.nameLowerCamelCase + " the " + field.fieldLabel);
        //                        Console.WriteLine("     */");
        //                        Console.WriteLine("    public void set" + field.nameUpperCamelCase + "(final " + getFieldType(field)
        //                                + " " + field.nameLowerCamelCase + ") {");
        //                        Console.WriteLine(
        //                                "        this." + field.nameLowerCamelCase + " = " + field.nameLowerCamelCase + ";");
        //                        Console.WriteLine("    }");

        //                    }

        //                    if (fields.size() > 0) {
        //                        // Console.WriteLine();
        //                        // Console.WriteLine(" @Override");
        //                        // Console.WriteLine(" public void setFieldValues(final Map<Integer, Object> values) {");
        //                        // for (final Field field : fields) {
        //                        // Console.WriteLine(" " + field.nameLowerCamelCase + " = (" + field.dataTypeClass
        //                        // + ") values.get(" + field.fieldId + ");");
        //                        // }
        //                        // Console.WriteLine(" }");

        //                        Console.WriteLine();
        //                        Console.WriteLine("    @Override");
        //                        Console.WriteLine("    public void serialize(final ZclFieldSerializer serializer) {");
        //                        Console.WriteLine("        super.serialize(serializer);");
        //                        Console.WriteLine();
        //                        for (final Field field : fields) {
        //                            if (getAutoSized(fields, field.nameLowerCamelCase) != null) {
        //                                Field sizedField = getAutoSized(fields, field.nameLowerCamelCase);
        //Console.WriteLine("        serializer.serialize(" + sizedField.nameLowerCamelCase
        //                                        + ".size(), ZclDataType." + field.dataType + ");");

        //                                continue;
        //                            }

        //                            if (field.listSizer != null) {
        //                                Console.WriteLine("        for (int cnt = 0; cnt < " + field.nameLowerCamelCase
        //                                        + ".size(); cnt++) {");
        //                                Console.WriteLine("            serializer.serialize(" + field.nameLowerCamelCase
        //                                        + ".get(cnt), ZclDataType." + field.dataType + ");");
        //                                Console.WriteLine("        }");
        //                            } else {
        //                                Console.WriteLine("        serializer.serialize(" + field.nameLowerCamelCase
        //                                        + ", ZclDataType." + field.dataType + ");");
        //                            }
        //                        }
        //                        Console.WriteLine("    }");

        //                        Console.WriteLine();
        //                        Console.WriteLine("    @Override");
        //                        Console.WriteLine("    public void deserialize(final ZclFieldDeserializer deserializer) {");
        //                        Console.WriteLine("        super.deserialize(deserializer);");
        //                        Console.WriteLine();
        //boolean first = true;
        //                        for (final Field field : fields) {
        //                            if (field.listSizer != null) {
        //                                if (first) {
        //                                    Console.WriteLine("        // Create lists");
        //first = false;
        //                                }
        //                                Console.WriteLine("        " + field.nameLowerCamelCase + " = new ArrayList<"
        //                                        + field.dataTypeClass + ">();");
        //                            }
        //                        }
        //                        if (first == false) {
        //                            Console.WriteLine();
        //                        }
        //                        for (final Field field : fields) {
        //                            if (field.completeOnZero) {
        //                                Console.WriteLine("        if (deserializer.isEndOfStream()) {");
        //                                Console.WriteLine("            return;");
        //                                Console.WriteLine("        }");
        //                            }
        //                            if (getAutoSized(fields, field.nameLowerCamelCase) != null) {
        //                                Console.WriteLine("        Integer " + field.nameLowerCamelCase + " = (" + field.dataTypeClass
        //                                        + ") deserializer.deserialize(" + "ZclDataType." + field.dataType + ");");
        //                                continue;
        //                            }

        //                            if (field.listSizer != null) {
        //                                Console.WriteLine("        if (" + field.listSizer + " != null) {");
        //                                Console.WriteLine("            for (int cnt = 0; cnt < " + field.listSizer + "; cnt++) {");
        //                                Console.WriteLine("                " + field.nameLowerCamelCase + ".add(("
        //                                        + field.dataTypeClass + ") deserializer.deserialize(" + "ZclDataType."
        //                                        + field.dataType + "));");
        //                                Console.WriteLine("            }");
        //                                Console.WriteLine("        }");
        //                            } else {
        //                                Console.WriteLine("        " + field.nameLowerCamelCase + " = (" + field.dataTypeClass
        //                                        + ") deserializer.deserialize(" + "ZclDataType." + field.dataType + ");");
        //                            }
        //                            if (field.completeOnZero) {
        //                                Console.WriteLine("        if (" + field.nameLowerCamelCase + " == 0) {");
        //                                Console.WriteLine("            return;");
        //                                Console.WriteLine("        }");
        //                            }

        //                            if (field.nameLowerCamelCase.equals("status")) {
        //                                Console.WriteLine("        if (status != ZdoStatus.SUCCESS) {");
        //                                Console.WriteLine("            // Don't read the full response if we have an error");
        //                                Console.WriteLine("            return;");
        //                                Console.WriteLine("        }");
        //                            }
        //                        }
        //                        Console.WriteLine("    }");
        //                    }

        //                    if (command.responseCommand != null && command.responseCommand.length() != 0) {
        //                        Console.WriteLine();
        //                        Console.WriteLine("    @Override");
        //                        Console.WriteLine(
        //                                "    public boolean isTransactionMatch(ZigBeeCommand request, ZigBeeCommand response) {");
        //                        Console.WriteLine("        if (!(response instanceof " + command.responseCommand + ")) {");
        //                        Console.WriteLine("            return false;");
        //                        Console.WriteLine("        }");
        //                        Console.WriteLine();
        //                        out.print("        return ");
        //// Console.WriteLine("(((" + command.nameUpperCamelCase + ") request).getDestinationAddress()");
        //// out.print(" .equals(((" + command.responseCommand
        //// + ") response).getSourceAddress()))");

        //boolean first = true;
        //                        for (string matcher : command.responseMatchers.keySet()) {
        //                            if (first == false) {
        //                                Console.WriteLine();
        //                                out.print("                && ");
        //                            }
        //                            first = false;
        //                            Console.WriteLine("(((" + command.nameUpperCamelCase + ") request).get" + matcher + "()");
        //                            out.print("                .equals(((" + command.responseCommand + ") response).get"
        //                                    + command.responseMatchers.get(matcher) + "()))");
        //                        }

        //                        // Default address checker
        //                        if (first == true) {
        //                            out.print("((ZdoRequest) request).getDestinationAddress().equals((("
        //                                    + command.responseCommand + ") response).getSourceAddress())");
        //                        }

        //                        out.print(";");
        //Console.WriteLine();
        //                        Console.WriteLine("    }");
        //                    }

        //                    int fieldLen = 0;
        //                    for (final Field field : fields) {
        //                        fieldLen += field.nameLowerCamelCase.length() + 20;
        //                    }

        //                    Console.WriteLine();
        //                    Console.WriteLine("    @Override");
        //                    Console.WriteLine("    public string tostring() {");
        //                    Console.WriteLine("        final stringBuilder builder = new stringBuilder("
        //                            + (className.length() + 3 + fieldLen) + ");");
        //                    Console.WriteLine("        builder.append(\"" + className + " [\");");
        //                    Console.WriteLine("        builder.append(super.tostring());");
        //                    for (final Field field : fields) {
        //                        if (getAutoSized(fields, field.nameLowerCamelCase) != null) {
        //                            continue;
        //                        }

        //                        Console.WriteLine("        builder.append(\", " + field.nameLowerCamelCase + "=\");");
        //                        Console.WriteLine("        builder.append(" + field.nameLowerCamelCase + ");");
        //                    }
        //                    Console.WriteLine("        builder.append(\']\');");
        //                    Console.WriteLine("        return builder.tostring();");
        //                    Console.WriteLine("    }");

        //                    Console.WriteLine();
        //                    Console.WriteLine("}");

        //                    out.flush();
        //                    out.close();
        //                }
        //            }
        //        }
        //    }

        //    private static Field getAutoSized(LinkedList<Field> fields, string name)
        //{
        //    for (Field field : fields)
        //    {
        //        if (name.equals(field.listSizer))
        //        {
        //            return field;
        //        }
        //    }
        //    return null;
        //}

        //private static string getZdoCommandTypeEnum(final Cluster cluster, final Command command, boolean received)
        //{
        //    return command.commandType + "(" + string.format("0x%04X", command.commandId) + ", "
        //            + command.nameUpperCamelCase + ".class" + ")";
        //}

        //private static string getZdpClusterCommandPackage(string packageRoot, Cluster cluster)
        //{
        //    return packageRoot + packageZdpCommand + "." + cluster.clusterType.replace("_", "").toLowerCase();
        //}

        //private static void generateZdoCommandTypeEnumeration(Context context, string packageRootPrefix,
        //        File sourceRootPath) throws IOException
        //{
        //    final string className = "ZdoCommandType";

        //    final string packageRoot = packageRootPrefix + packageZdp;
        //    final string packagePath = getPackagePath(sourceRootPath, packageRoot);
        //    final File packageFile = getPackageFile(packagePath);

        //    final PrintWriter out = getClassOut(packageFile, className);

        //    CodeGeneratorUtil.outputLicense(out);

        //    Console.WriteLine("package " + packageRoot + ";");
        //    Console.WriteLine();

        //    Map<string, Command> commandEnum = new TreeMap<string, Command>();

        //        final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                // Brute force to get the commands in order!
        //                for (int c = 0; c< 65535; c++) {
        //                    if (cluster.received.get(c) != null) {
        //                        Console.WriteLine("import " + packageRootPrefix + packageZdpCommand + "."
        //                                + cluster.received.get(c).nameUpperCamelCase + ";");

        //commandEnum.put(getZdoCommandTypeEnum(cluster, cluster.received.get(c), true),
        //                                cluster.received.get(c));
        //                    }
        //                }
        //            }
        //        }
        //        Console.WriteLine();

        //        Console.WriteLine();
        //outputClassJavaDoc(out, "Enumeration of ZDP commands");
        //Console.WriteLine("public enum " + className + " {");
        //boolean first = true;
        //        for (string command : commandEnum.keySet()) {
        //            Command cmd = commandEnum.get(command);
        //            if (cmd == null) {
        //                Console.WriteLine("Command without data: " + command);
        //                continue;
        //            }

        //            if (!first) {
        //                Console.WriteLine(",");
        //            }
        //            first = false;
        //            Console.WriteLine("    /**");
        //            Console.WriteLine("     * " + cmd.commandLabel);
        //            Console.WriteLine("     * <p>");
        //            Console.WriteLine("     * See {@link " + cmd.nameUpperCamelCase + "}");
        //            Console.WriteLine("     */");
        //            out.print("    " + command);
        //        }
        //        Console.WriteLine(";");

        //        Console.WriteLine();
        //        Console.WriteLine("    private final int clusterId;");
        //        Console.WriteLine("    private final Class<? extends ZdoCommand> commandClass;");
        //        Console.WriteLine();
        //        Console.WriteLine("    " + className + "(final int clusterId, final Class<? extends ZdoCommand> commandClass) {");
        //        Console.WriteLine("        this.clusterId = clusterId;");
        //        Console.WriteLine("        this.commandClass = commandClass;");
        //        // Console.WriteLine(" this.label = label;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();

        //        Console.WriteLine("    public int getClusterId() {");
        //        Console.WriteLine("        return clusterId;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public Class<? extends ZdoCommand> getCommandClass() {");
        //        Console.WriteLine("        return commandClass;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine();
        //        Console.WriteLine("    public static ZdoCommandType getValueById(final int clusterId) {");
        //        Console.WriteLine("        for (final ZdoCommandType value : values()) {");
        //        Console.WriteLine("            if(value.clusterId == clusterId) {");
        //        Console.WriteLine("                return value;");
        //        Console.WriteLine("            }");
        //        Console.WriteLine("        }");
        //        Console.WriteLine("        return null;");
        //        Console.WriteLine("    }");
        //        Console.WriteLine("}");

        //        out.flush();
        //        out.close();
        //    }

        //    private static void generateZdpCommandTransactions(Context context, string packageRootPrefix, File sourceRootPath)
        //            throws IOException
        //{

        //    final LinkedList<Profile> profiles = new LinkedList<Profile>(context.profiles.values());
        //        for (final Profile profile : profiles) {
        //            final LinkedList<Cluster> clusters = new LinkedList<Cluster>(profile.clusters.values());
        //            for (final Cluster cluster : clusters) {
        //                final ArrayList<Command> commands = new ArrayList<Command>();
        //commands.addAll(cluster.received.values());
        //                commands.addAll(cluster.generated.values());
        //                for (final Command command : commands) {
        //                    if (command.responseCommand == null || command.responseCommand.length() == 0) {
        //                        continue;
        //                    }

        //                    final string packageRoot = packageRootPrefix + packageZdpTransaction;
        //final string packagePath = getPackagePath(sourceRootPath, packageRoot);
        //final File packageFile = getPackageFile(packagePath);

        //final string className = command.nameUpperCamelCase + "Transaction";
        //final PrintWriter out = getClassOut(packageFile, className);

        //Console.WriteLine("package " + packageRoot + ";");
        //                    Console.WriteLine();

        //                    // Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclCommandMessage;");
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZclCommand;");
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZcl + ".ZclField;");

        //                    Console.WriteLine(
        //                            "import " + packageRootPrefix + packageZdpCommand + "." + command.nameUpperCamelCase + ";");
        //                    Console.WriteLine(
        //                            "import " + packageRootPrefix + packageZdpCommand + "." + command.responseCommand + ";");

        //                    Console.WriteLine("import " + packageRootPrefix + ".Command;");
        //                    Console.WriteLine(
        //                            "import " + packageRootPrefix + packageTransactionPrefix + ".ZigBeeTransactionMatcher;");
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZdoRequest;");
        //                    // Console.WriteLine("import " + packageRootPrefix + packageZdp + ".ZdoResponse;");

        //                    // Console.WriteLine("import java.util.Map;");
        //                    // Console.WriteLine("import java.util.HashMap;");

        //                    Console.WriteLine();
        //                    Console.WriteLine("/**");
        //                    Console.WriteLine(" * " + command.commandLabel + " transaction class.");

        //                    if (command.commandDescription != null && command.commandDescription.size() != 0) {
        //                        Console.WriteLine(" * <p>");
        //outputWithLinebreak(out, "", command.commandDescription);
        //                    }

        //                    if (cluster.clusterDescription.size() > 0) {
        //                        Console.WriteLine(" * <p>");
        //outputWithLinebreak(out, "", cluster.clusterDescription);
        //                    }

        //                    Console.WriteLine(" * <p>");
        //                    Console.WriteLine(" * Code is auto-generated. Modifications may be overwritten!");

        //                    Console.WriteLine(" */");
        //                    Console.WriteLine("public class " + className + " implements ZigBeeTransactionMatcher {");

        //                    // Console.WriteLine(" /**");
        //                    // Console.WriteLine(" * Default constructor.");
        //                    // Console.WriteLine(" */");
        //                    // Console.WriteLine(" public " + className + "() {");
        //                    // Console.WriteLine(" }");
        //                    // Console.WriteLine(" setType(ZclCommandType." + command.commandType + ");");
        //                    // Console.WriteLine(" commandId = " + command.commandId + ";");
        //                    // Console.WriteLine(" commandDirection = "
        //                    // + (cluster.received.containsValue(command) ? "true" : "false") + ";");

        //                    Console.WriteLine();
        //                    Console.WriteLine("    @Override");
        //                    Console.WriteLine("    public boolean isTransactionMatch(Command request, Command response) {");
        //                    Console.WriteLine("        if (response instanceof " + command.responseCommand + ") {");
        //                    // Console.WriteLine(" return ((" + command.nameUpperCamelCase + ") request).get"
        //                    // + command.responseRequest + "() == ((" + command.responseCommand + ") response).get"
        //                    // + command.responseResponse + "();");
        //                    Console.WriteLine("        } else {");
        //                    Console.WriteLine("            return false;");
        //                    Console.WriteLine("        }");
        //                    Console.WriteLine("    }");

        //                    Console.WriteLine();
        //                    Console.WriteLine("}");

        //                    out.flush();
        //                    out.close();
        //                }
        //            }
        //        }
        //    }
        //    }
    }
}
