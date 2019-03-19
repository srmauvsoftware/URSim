using System.Collections;
using SimpleJSON;

/**
 * This defines a subscriber. Subscribers listen to publishers in ROS. Now if we could have inheritance
 * on static classes then we could do this differently. But basically, you have to make up one of these
 * for every subscriber you need.
 * 
 * Subscribers require a ROSBridgePacket to subscribe to (its type). They need the name of
 * the message, and they need something to draw it. 
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.1
 */

namespace ROSBridgeLib {
	public class ROSBridgeSubscriber {

		public static string GetMessageTopic() {
			return null;
		}  

		public static string GetMessageType() {
			return null;
		}

		public static ROSBridgeMsg ParseMessage(JSONNode msg) {
			return null;
		}

		public static void CallBack(ROSBridgeMsg msg) {
		}
	}
}

