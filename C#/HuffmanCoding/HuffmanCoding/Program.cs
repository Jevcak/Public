using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;

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
                byte[] header = new byte[] { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };
                byte[] end = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                var fout = new BinaryWriter(File.Open(args[0] + ".huff", FileMode.Create, FileAccess.Write));
                var byteReader = new ByteReader(fileName);
                var treePrefixWriter = new TreePrefixWriter(Console.Out);
                var encodedWriter = new HuffmanEncodedWriter(Console.Out);
                var frequencyList = new ulong[256];
                byteReader!.InitializeFileReader();
                byte b = byteReader!.ReadNextByte();
                //If there is an empty file write nothing and return
                if (byteReader.GetFileSize() == 0)
                    return;
                //While there is something in the file get it into the frequency dictionary
                while (!byteReader.EOF)
                {
                    frequencyList[b] = frequencyList[b] + 1;
                    b = byteReader!.ReadNextByte();
                }
                //Build a Huffman from the dictionary
                var huffmanTree = new HuffmanTree();
                huffmanTree.BuildTree(frequencyList);
                encodedWriter!.EncodeAndWrite(huffmanTree.root, huffmanTree.root);
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
    public interface Encoder
    {
        public byte[] Encode(HuffmanNode node);
    }
    public struct Bytes
    {
        public byte[] bytes { get; }
        public Bytes(byte[] bytes)
        {
            this.bytes = bytes;
        }
    }
    public abstract class HuffmanNode
    {
        public ulong weight { get; set; }
        public int time;
        public abstract string GetID();
    }
    public class HuffmanInnerNode : HuffmanNode
    {
        public HuffmanNode? left { get; set; }
        public HuffmanNode? right { get; set; }
        public HuffmanInnerNode(ulong w, HuffmanNode? leftChild, HuffmanNode? rightChild, int t)
        {
            weight = w;
            left = leftChild;
            right = rightChild;
            time = t;
        }
        public override string GetID()
        {
            return weight.ToString();
        }
    }
    public class HuffmanLeaf : HuffmanNode
    {
        public byte symbol { get; set; }

        public HuffmanLeaf(byte character, ulong w)
        {
            symbol = character;
            weight = w;
            time = -1;
        }
        public override string GetID()
        {
            return string.Format("*{0}:{1}", (ulong)symbol, weight);
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
            else if (x is HuffmanLeaf && y is HuffmanLeaf)
            {
                return ((HuffmanLeaf)x).symbol.CompareTo(((HuffmanLeaf)y).symbol);
            }
            else if (x is HuffmanLeaf)
            {
                return -1;
            }
            else if (y is HuffmanLeaf)
            {
                return 1;
            }
            return x.time.CompareTo(y.time);
        }
    }

    public class HuffmanTree
    {
        public HuffmanNode root { get; set; } = new HuffmanLeaf(0, 0);

        public void BuildTree(ulong[] frequencyDictionary)
        {
            var minHeap = new PriorityQueue<HuffmanNode, HuffmanNode>(new HuffmanNodeComparator());
            int time = 0;
            //prepare a forest of single leaf nodes, make a minimal heap according to the priority
            for (int i = 0; i < 256; i++)
            {
                if (frequencyDictionary[i] != 0)
                {
                    var newLeaf = new HuffmanLeaf((byte)i, frequencyDictionary[i]);
                    minHeap.Enqueue(newLeaf, newLeaf);
                }
            }
            //while it is a forest merge the high priority nodes
            while (minHeap.Count > 1)
            {
                var leftChild = minHeap.Dequeue();
                var rightChild = minHeap.Dequeue();

                var mergedNode = new HuffmanInnerNode(leftChild.weight + rightChild.weight, leftChild, rightChild, ++time);
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
        public ulong GetFileSize()
        {
            return (ulong)reader!.Length;
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
            if (node is HuffmanInnerNode)
            {
                writer!.Write(nodeID);
                WriteTreeInPrefixNotation(((HuffmanInnerNode)node).left!, root);
                WriteTreeInPrefixNotation(((HuffmanInnerNode)node).right!, root);
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
    public class HuffmanEncoder : Encoder
    {
        public byte[] Encode(HuffmanNode node)
        {
            byte[] var = new byte[2] {0,0 };
            return var;
        }
        ulong encodeInnerNode(HuffmanInnerNode node)
        {
            ulong result = node.weight;
            result <<= 1;
            result &= 0x00fffffffffffffe;
            return result;
        }
        ulong encodeLeafNode(HuffmanLeaf node)
        {
            ulong result = node.weight;
            result <<= 1;
            result |= 0xff00000000000001;
            result &= ~(~(ulong)node.symbol << 56);
            return result;
        }
    }
    public class HuffmanEncodedWriter : TextWriter
    {
        private TextWriter? writer;
        public override Encoding Encoding => writer!.Encoding;
        public HuffmanEncodedWriter(TextWriter writer)
        {
            this.writer = writer;
        }
        public void EncodeAndWrite(HuffmanNode node, HuffmanNode root)
        {
        }
    }
}