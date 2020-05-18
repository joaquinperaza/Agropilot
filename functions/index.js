const functions = require('firebase-functions');

// // Create and Deploy Your First Cloud Functions
// // https://firebase.google.com/docs/functions/write-firebase-functions
//
// exports.helloWorld = functions.https.onRequest((request, response) => {
//  response.send("Hello from Firebase!");
// });

const admin = require('firebase-admin');
admin.initializeApp();

const db = admin.firestore();

exports.logger = functions.firestore
.document('/status/data').onUpdate((change, context) => {
	const newValue = change.after.data();
	db.collection('/log').add(newValue);
});
