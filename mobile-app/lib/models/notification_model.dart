class NotificationModel {
  final int id;
  final String title;
  final String body;
  final String createdAt;
  final String sentBy;
  final bool isSent;

  NotificationModel({
    required this.id,
    required this.title,
    required this.body,
    required this.createdAt,
    required this.sentBy,
    required this.isSent,
  });

  factory NotificationModel.fromJson(Map<String, dynamic> json) {
    return NotificationModel(
      id: json['id'] as int,
      title: json['title'] as String,
      body: json['body'] as String,
      createdAt: json['createdAt'] as String,
      sentBy: json['sentBy'] as String,
      isSent: json['isSent'] as bool,
    );
  }
}
