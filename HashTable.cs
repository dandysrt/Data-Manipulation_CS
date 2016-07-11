using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manipulation
{
    class HashTable<T>
    {
		private LinkList<T>[] hashList;
		private int listMod;
		
		public HashTable(int size){
			hashList = new LinkList<T>[size];
			for(int i = 0; i < hashList.Length; i++)
				hashList[i] = new LinkList<T>();
			listMod = size;
		}
		
		
		//can be a rapid shortcut for retrieving data if key has no collisions
		public T get(int key){
			LinkList<T> output = hashList[key%listMod];
			if(output != null){
				if(output.head == null){
					return default(T);
				}else if(output.head.next != null){
					Console.Out.Write("Multiple values detected at key resolution: %s, "
							+ " please specify the data you wish to retrieve"
							+ " USAGE: 'get(key, data)\n",( key%listMod));
					return default(T);
				}else{
					return output.head.data;
				}
			}
			return default(T);
			
		}
		
		//longest search method if key is unknown O(n^2) at worst
		public T get(T data){
			for(int i = 0; i < hashList.Length; i++){	
				LinkList<T>.Node<T> output = (LinkList<T>.Node<T>)hashList[i].find(data);
				if(output != null)
					if(output.data != null)
						return output.data;
					
			}
			return default(T);
		}
		
		//most rapid search if key is known
		public T get(int key, T data){
			return hashList[key%listMod].find(data).data;
		}
		
		public void add(int key, T data){
			if(hashList[key%listMod] == null)
				hashList[key%listMod] = new LinkList<T>();
			hashList[key%listMod].add(key, data);
		}
		
		public void remove(int key, T data){
			hashList[key%listMod].remove(data);
		}
		
		//unused will be implemented for dynamic list
		private void updateListMod(){
			listMod = hashList.Length;
		}
		
		//Doubles the size of the hash table when called
		public void grow(){
			LinkList<T>[] tempList = new LinkList<T>[(hashList.Length * 2)];
			int i ;
			for( i = 0; i < hashList.Length; i++){
				tempList[i] = new LinkList<T>();
				if(hashList[i] != null){
					LinkList<T>.Node<T> tempNode = (LinkList<T>.Node<T>)hashList[i].head;
					if(tempNode != null){
						while(tempNode.data != null){
							if(tempList[tempNode.key%tempList.Length] == null)
								tempList[tempNode.key%tempList.Length] = new LinkList<T>();
							tempList[tempNode.key%tempList.Length].add(tempNode.data);
							if(tempNode.next != null)
								tempNode = (LinkList<T>.Node<T>) tempNode.next;
							else 
								tempNode.data = default(T);
						}
						tempNode = null;
					}
				}
			}
			hashList = tempList;
			updateListMod();
		}
	
		public void printTable(){
			for(int i = 0; i < hashList.Length; i++){
				if(hashList[i] != null){
					if(hashList[i].head != null) Console.Out.Write("%s: ", i);
					hashList[i].printList();
				}else{
					hashList[i] = new LinkList<T>();
				}
			}
		}
		
		/*=========================SUBCLASSES===========================*/
		
		public class LinkList<T> : LinkedList<T>{
			public void add(int key, T data){
				if(head == null){
                    head = new Node<T>(null, key, data, (Node<T>)tail);
                    tail = head;
					return;
				}
				Node<T> newNode = new Node<T>((Node<T>)tail, key, data, null);
                tail.next = newNode;
                tail = newNode;
			}
			
			public class Node<T> : LinkedList<T>.Node<T>{
				public int key;
				public Node(Node<T> prev, int key, T data, Node<T> next) : base(prev, data, next){
					this.key = key;
				}
			}
			
		}
    }
}
