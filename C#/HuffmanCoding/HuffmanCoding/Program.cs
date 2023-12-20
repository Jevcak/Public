using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

#nullable enable

namespace HuffmanCoding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Check argument count
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
                return;
            }
            string fileName = args[0];
            try
            {
                var byteReader = new ByteReader(fileName);
                var treePrefixWriter = new TreePrefixWriter(Console.Out);
                var frequencyDictionary = new Dictionary<byte, int>();
                byteReader!.InitializeFileReader();

                byte b = byteReader!.ReadNextByte();
                //If there is an empty file write nothing and return
                if (byteReader.GetFileSize() == 0)
                    return;
                //While there is something in the file get it into the frequency dictionary
                while (!byteReader.EOF)
                {
                    _ = frequencyDictionary.TryGetValue(b, out int value);
                    value++;
                    frequencyDictionary[b] = value;
                    b = byteReader!.ReadNextByte();
                }
                //Build a Huffman from the dictionary
                var huffmanTree = new HuffmanTree();
                huffmanTree.BuildTree(frequencyDictionary);
                treePrefixWriter!.WriteTreeInPrefixNotation(huffmanTree.root, huffmanTree.root);
                treePrefixWriter.Dispose();
                byteReader?.Dispose();
            }
            //if there's an error it should be a File Error
            catch (Exception)
            {
                Console.WriteLine("File Error");
            }
        }
    }

    public class HuffmanNode
    {
        public byte symbol { get; set; }
        public int weight { get; set; }
        public bool leaf;
        public int time;
        public HuffmanNode? left { get; set; }
        public HuffmanNode? right { get; set; }
        public HuffmanNode(int w, HuffmanNode? leftChild, HuffmanNode? rightChild, int t)
        {
            weight = w;
            left = leftChild;
            right = rightChild;
            leaf = false;
            time = t;
        }
        public HuffmanNode(byte character, int w)
        {
            symbol = character;
            weight = w;
            leaf = true;
            time = -1;
        }
        public string GetID()
        {
            if (this.leaf)
                return string.Format("*{0}:{1}", (int)symbol, weight);
            else
                return weight.ToString();
        }

    }

    public class HuffmanNodeComparator : IComparer<HuffmanNode>
    {
        public int Compare(HuffmanNode? x, HuffmanNode? y)
        {
            if (x!.weight != y!.weight)
            {
                return x.weight.CompareTo(y.weight);
            }
            else if (x.leaf && y.leaf)
            {
                return x.symbol.CompareTo(y.symbol);
            }
            else if (x.leaf)
            {
                return -1;
            }
            else if (y.leaf)
            {
                return 1;
            }
            return x.time.CompareTo(y.time);
        }
    }

    public class HuffmanTree
    {
        public HuffmanNode root { get; set; } = new HuffmanNode(0, 0);

        public void BuildTree(Dictionary<byte, int> frequencyDictionary)
        {
            if (frequencyDictionary.Count < 1)
                return;

            var minHeap = new PriorityQueue<HuffmanNode, HuffmanNode>(new HuffmanNodeComparator());
            int time = 0;
            //prepare a forest of single leaf nodes, make a minimal heap according to the priority
            foreach (var item in frequencyDictionary)
            {
                var newLeaf = new HuffmanNode(item.Key, item.Value);
                minHeap.Enqueue(newLeaf, newLeaf);
            }
            //while it is a forest merge the high priority nodes
            while (minHeap.Count > 1)
            {
                var leftChild = minHeap.Dequeue();
                var rightChild = minHeap.Dequeue();

                var mergedNode = new HuffmanNode(leftChild.weight + rightChild.weight, leftChild, rightChild, ++time);
                minHeap.Enqueue(mergedNode, mergedNode);
            }

            root = minHeap.Dequeue();
        }
    }

    public class ByteReader
    {
        private FileStream? reader;
        private string fileName;
        public bool EOF { get; set; } = false;
        public ByteReader(string path)
        {
            fileName = path;
        }
        public long GetFileSize()
        {
            return reader!.Length;
        }
        public void InitializeFileReader()
        {
            reader = File.OpenRead(fileName);
        }

        public byte ReadNextByte()
        {
            int b = reader!.ReadByte();
            if (b == -1)
            {
                EOF = true;
            }
            return (byte)b;
        }

        public void Dispose()
        {
            reader?.Dispose();
        }
    }

    public class TreePrefixWriter : TextWriter
    {
        private TextWriter? writer;
        public override Encoding Encoding => writer!.Encoding;

        public TreePrefixWriter(TextWriter writer)
        {
            this.writer = writer;
        }
        public void WriteTreeInPrefixNotation(HuffmanNode node, HuffmanNode root)
        {
            if (!ReferenceEquals(node, root))
            {
                writer!.Write(" ");
            }
            string nodeID = node.GetID();
            if (!node.leaf)
            {
                writer!.Write(nodeID);
                WriteTreeInPrefixNotation(node.left!, root);
                WriteTreeInPrefixNotation(node.right!, root);
            }
            else
            {
                writer!.Write(nodeID);
            }
        }

        public new void Dispose()
        {
            writer!.Dispose();
        }

    }
}