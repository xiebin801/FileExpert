﻿using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExpert.MP4.Box
{
    class MovieExtendsHeaderBox : FullBox
    {
        public MovieExtendsHeaderBox(TreeView treeView, TreeNode parent, DataStore dataStore, ref Int64 bitOffset) :
            base(treeView, parent, dataStore, ref bitOffset)
        {
            TreeNode nodeBox = null;
            TreeNode newNode = null;

            Int64 firstOffset = bitOffset;

            //Add one node to indicate this box.
            Result result = Utility.AddNodeContainer(Position.CHILD, parent, out nodeBox, "MovieExtendsHeaderBox_payload", ItemType.SECTION1, dataStore, bitOffset, dataStore.GetLeftBitLength());

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                if (1 == version)
                {
                    if (result.Fine)
                    {
                        //unsigned int(64) fragment_duration;
                        result = Utility.AddNodeField(Position.CHILD, nodeBox, out newNode, "fragment_duration", ItemType.FIELD, dataStore, ref bitOffset, 64, ref fieldValue);
                    }
                }
                else
                {
                    if (result.Fine)
                    {
                        //unsigned int(32) fragment_duration;
                        result = Utility.AddNodeField(Position.CHILD, nodeBox, out newNode, "fragment_duration", ItemType.FIELD, dataStore, ref bitOffset, 32, ref fieldValue);
                    }
                }
            }

            if (result.Fine)
            {
                Utility.UpdateNodeLength(nodeBox, "MovieExtendsHeaderBox_payload", ItemType.SECTION1, bitOffset - firstOffset);
            }
        }
    }
}
