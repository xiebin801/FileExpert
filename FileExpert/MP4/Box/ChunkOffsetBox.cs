﻿using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExpert.MP4.Box
{
    class ChunkOffsetBox : FullBox
    {
        public ChunkOffsetBox(TreeView treeView, TreeNode parent, DataStore dataStore, ref Int64 bitOffset) :
            base(treeView, parent, dataStore, ref bitOffset)
        {
            TreeNode nodeBox = null;
            TreeNode newNode = null;

            Int64 firstOffset = bitOffset;

            //Add one node to indicate this box.
            Result result = Utility.AddNodeContainer(Position.CHILD, parent, out nodeBox, "ChunkOffsetBox_payload", ItemType.SECTION1, dataStore, bitOffset, dataStore.GetLeftBitLength());

            Int64 fieldValue = 0;

            Int64 entryCount = 0;
            if (result.Fine)
            {
                //unsigned int(32) entry_count;
                result = Utility.AddNodeField(Position.CHILD, nodeBox, out newNode, "entry_count", ItemType.FIELD, dataStore, ref bitOffset, 32, ref entryCount);
            }

            if (result.Fine)
            {
                for (int i = 0; i < entryCount; i++)
                {
                    if (result.Fine)
                    {
                        //unsigned int(32) chunk_offset;
                        result = Utility.AddNodeField(Position.CHILD, nodeBox, out newNode, "chunk_offset", ItemType.FIELD, dataStore, ref bitOffset, 32, ref fieldValue);
                    }
                }

            }

            if (result.Fine)
            {
                Utility.UpdateNodeLength(nodeBox, "ChunkOffsetBox_payload", ItemType.SECTION1, bitOffset - firstOffset);
            }
        }
    }
}
