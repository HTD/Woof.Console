using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Woof.ConsoleEx.ConsoleFilters {

    /// <summary>
    /// Introduces a delay to console output.
    /// </summary>
    public class Delay : TextWriter {

        /// <summary>
        /// Character delay in milliseconds.
        /// </summary>
        readonly int Ms;

        /// <summary>
        /// Creates the filter over text output.
        /// </summary>
        /// <param name="ms">Character delay in milliseconds.</param>
        public Delay(int ms = 16) => Ms = ms;

        /// <summary>
        /// Gets bound console encoding.
        /// </summary>
        public override Encoding Encoding => Console.OutputEncoding;

        /// <summary>
        /// Writes a character to the console or tries parse it.
        /// </summary>
        /// <param name="c"></param>
        public override void Write(char c) {
            Out.Write(c);
            Thread.Sleep(Ms);
        }

        /// <summary>
        /// Console out text writer.
        /// </summary>
        readonly TextWriter Out = Console.Out;

    }

}