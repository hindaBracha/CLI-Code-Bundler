CLI Code Bundler | C#, System.CommandLine | Efficient Code Organization

Developed a robust Command Line Interface (CLI) tool designed for bundling and organizing code files efficiently. This tool simplifies the process of consolidating source code into a single file while offering extensive customization options for developers.

Key Features:
Customizable File Selection: Filters files based on specified extensions or languages (.cs, .js, .html, etc.), with an option to include all supported file types.
Sorting Capabilities: Enables sorting by file extensions or file names to improve readability and navigation.
Content Optimization: Provides an option to remove blank lines, ensuring clean and compact output files.
File Metadata: Includes optional file path annotations and developer notes for better traceability and documentation.
Automated Response File Creation: Generates reusable response files (.rsp) for quicker execution of repetitive commands.
Secure and Efficient: Handles files using asynchronous operations for faster processing and avoids system-sensitive directories like bin, obj, and node_modules.
Design Highlights:
Built with System.CommandLine for intuitive CLI interactions and argument handling.
Utilized Regex for dynamic content modification, such as removing blank lines.
Ensured error handling to manage edge cases, such as invalid output paths or missing files, with clear user feedback.
Implemented StringBuilder to optimize memory usage when processing large numbers of files.
Practical Use Cases:
This tool is ideal for software teams and developers looking to archive, share, or audit project files, as it aggregates all relevant source code into a single, neatly organized file.

Example Command:
bundle --output bundled_code.txt --language .cs .js --sort extensions --emptyrow y --note y --author "Your Name"
Generates a bundled file of .cs and .js files, sorted by extensions, with blank lines removed and file paths annotated.

This project emphasizes usability, modularity, and efficiency, making it a valuable asset for managing source code repositories.
